import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-dropdown',
  templateUrl: './user-dropdown.component.html',
  styleUrls: ['./user-dropdown.component.css']
})
export class UserDropdownComponent implements OnInit {
  @ViewChild('dropdownToggle') dropdownToggle!: ElementRef;
  dropdownOpen: boolean = false;
  isAdmin: boolean = false;
  user: any = {};
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.user = this.authService.getUser();
    this.isAdmin = this.authService.getUserRole() === "Admin" ? true : false;
    //console.log(this.user.getValue());
  }

  toggleDropdown(): void {
    this.dropdownOpen = !this.dropdownOpen;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
