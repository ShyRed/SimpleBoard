import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import User from 'src/app/models/user';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styles: []
})
export class HeaderComponent implements OnInit {

  collapsed: boolean = true;
  user: User;

  constructor(private router: Router,
    private userService: UserService) { }

  ngOnInit() {
    this.userService.getLoggedInUser().subscribe(success => {
      this.user = success;
    },
    error => {
      this.user = null;
    });
  }

  onLogoutClick(): void {
    this.userService.logout();
    this.user = null;
    this.router.navigateByUrl("/");
  }

  toggleCollapsed(): void {
    this.collapsed = !this.collapsed;
  }
}
