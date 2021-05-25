import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Local } from 'protractor/built/driverProviders';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  userId: string;
  userName: string;

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
    this.userId = localStorage.getItem('userId');
    this.userName = localStorage.getItem('userName');
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  authenticated() {
    return localStorage.getItem('token') != null;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');
    this.router.navigate(['/account/login']);
  }
}
