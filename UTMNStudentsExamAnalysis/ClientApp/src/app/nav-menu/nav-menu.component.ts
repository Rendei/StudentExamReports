import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  hideNavbar: boolean = false;

  constructor(private router: Router) {
    // Check the current route to determine whether to hide the navbar
    this.router.events.subscribe(() => {
      this.hideNavbar = this.router.url.includes('login') || this.router.url.includes('register');
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
