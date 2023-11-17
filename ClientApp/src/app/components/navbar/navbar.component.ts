import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Subscription, tap } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  isCollapsed = true;
  isLogoutVisible: boolean;

  constructor(private authService: AuthService, private route: Router){}

  ngOnInit(): void {
    this.authService.isAuthenticated.subscribe(value => {
      this.isLogoutVisible = value;
    })
  }

  logoutButtonClickHandler(): void {
    this.authService.logout();
    this.route.navigate(['']);
    this.authService.isAuthenticated.next(false);
  }
}

