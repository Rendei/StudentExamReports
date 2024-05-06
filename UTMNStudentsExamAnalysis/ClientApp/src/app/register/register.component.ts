import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @ViewChild('registerButton')
    registerButton!: HTMLButtonElement;

  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(email: string, password: string): void {
    let user = { email, password };
    this.userService.addUser(user)
      .subscribe(response => {
        console.log(`User added, ${user}`);
      },
        error => {
          // TODO Handle registration error (display error message)
          this.toastr.error('Registration failed. Please try again.', error.error);
          console.error('Registration failed:', error.error);
        });
  }
}
