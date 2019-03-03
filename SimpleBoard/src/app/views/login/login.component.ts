import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user.service';

import User from '../../models/user';
import { DirectiveRegistryValuesIndex } from '@angular/core/src/render3/interfaces/styling';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  errorMessage: string;

  get username() { return this.loginForm.get('username'); }
  get password() { return this.loginForm.get('password'); }

  constructor(private router: Router,
    private userService: UserService) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
      "username": new FormControl("", [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(32)
      ]),
      "password": new FormControl("", [
        Validators.required,
        Validators.minLength(8)
      ])
    });

    if (this.userService.isAuthenticated())
      this.router.navigateByUrl("/board");
  }

  onSubmit() {
    this.errorMessage = null;
    let user  = this.loginForm.value as User;
    this.userService.login(user).subscribe(success => {
      if (success == null)
        this.errorMessage = "Login failed. Wrong user / password?";
      else
        this.router.navigateByUrl("/board");
    });
  }

}
