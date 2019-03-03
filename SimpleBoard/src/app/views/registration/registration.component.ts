import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import User from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { routerNgProbeToken } from '@angular/router/src/router_module';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html'
})
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup;
  errorMessage: string;

  get firstname() { return this.registrationForm.get("firstname"); }
  get lastname() { return this.registrationForm.get("lastname"); }
  get username() { return this.registrationForm.get("username"); }
  get password() { return this.registrationForm.get("password"); }
  get passwordverify() { return this.registrationForm.get("passwordverify"); }

  constructor(private userService: UserService,
    private router: Router) { }

  ngOnInit() {
    this.registrationForm = new FormGroup({
      "firstname": new FormControl("", [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(32)
      ]),
      "lastname": new FormControl("", [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(32)
      ]),
      "username": new FormControl("", [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(32)
      ]),
      "password": new FormControl("", [
        Validators.required,
        Validators.minLength(8)
      ]),
      "passwordverify": new FormControl()
    },
    {
      validators: this.passwordVerifyValidator
    });
  }

  passwordVerifyValidator(control: FormGroup): any {
    const pass = control.get("password");
    const verf = control.get("passwordverify");
    return pass && verf && pass.value != verf.value ? { "passwordMissmatch": true } : null;
  }

  onSubmit(): void {
    this.errorMessage = null;
    let user: User = {
      id: 0,
      firstName: this.firstname.value,
      lastName: this.lastname.value,
      username: this.username.value,
      password: this.password.value,
      token: ""
    };
    this.userService.register(user).subscribe(success => {
      this.userService.login(user).subscribe(x => {
        this.router.navigateByUrl("/board");
      },
      error => {
        this.errorMessage = "Failed to login after registration.";
      });
    },
    error => {
      this.errorMessage = "Error during registration. Please try again using a different username.";
    });
  }
}
