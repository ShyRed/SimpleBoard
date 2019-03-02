import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private userService: UserService,
    private router: Router) { }

  canActivate(): boolean {
    if(!this.userService.isAuthenticated()) {
      this.router.navigate(["/"]);
      return;
    }
    return true;
  }
}
