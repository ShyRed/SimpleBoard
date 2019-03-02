import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';

import User from '../models/user';

const url:string = "http://localhost:5000/api/";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private loggedInUser: User;
  private loggedInUserSource = new Subject<User>();
  public loggedInUser$ = this.loggedInUserSource.asObservable();

  constructor(private http: HttpClient,
    private jwtHelper: JwtHelperService) { }

  isAuthenticated(): boolean {
    let token = localStorage.getItem("token");
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  getLoggedInUser(): Observable<User> {
    if (this.loggedInUser == null) {
      this.http.get<User>(`${url}login`).subscribe(x => {
        this.setLoggedInUser(x);
      });
    }
    return this.loggedInUser$;
  }

  login(user: User): Observable<User> {
    this.http.post<User>(`${url}Login`, user).subscribe(x => { 
      this.setLoggedInUser(x);
      localStorage.setItem("token", x.token);
     });
     return this.loggedInUser$;
  }

  logout(): void {
    localStorage.clear();
    this.setLoggedInUser(null);
  }

  register(user: User): Observable<any> {
    return this.http.post<User>(url, user);
  }

  private setLoggedInUser(user: User)
  {
    this.loggedInUser = user;
    this.loggedInUserSource.next(user);
  }
}
