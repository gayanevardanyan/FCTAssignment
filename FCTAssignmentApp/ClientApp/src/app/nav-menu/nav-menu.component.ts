import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services';
import { Observable } from 'rxjs';
import { User } from '../models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
    }
    isExpanded = false;

    isLoggedIn$: Observable<boolean>;
    loggedInUser$: Observable<User>;
    ngOnInit() {
        this.isLoggedIn$ = this.authenticationService.isUserLoggedIn;
        this.loggedInUser$ = this.authenticationService.loggedInUser;
    }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}
