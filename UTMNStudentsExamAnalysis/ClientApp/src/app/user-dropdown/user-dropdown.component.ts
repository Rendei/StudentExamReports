import { Component, OnInit } from '@angular/core';
import { Dropdown } from 'bootstrap';

@Component({
  selector: 'app-user-dropdown',
  templateUrl: './user-dropdown.component.html',
  styleUrls: ['./user-dropdown.component.css']
})
export class UserDropdownComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    const dropdownMenuButton = document.getElementById('userDropdownMenuButton');
    if (dropdownMenuButton) {
      // Initialize dropdown only if the element exists
      new Dropdown(dropdownMenuButton);
    } else {
      console.error("Dropdown menu button element not found.");
    }
  }

}
