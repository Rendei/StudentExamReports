import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @ViewChild('registerButton')
    registerButton!: HTMLButtonElement;

  constructor(private authService: AuthService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(email: string, password: string): void{
    let user = { email, password };
    this.authService.register(user)
      .subscribe(response => {
        // TODO Handle successful registration (e.g., store JWT token)
        console.log('Registration successful:', response.token);
      },
        error => {
          // TODO Handle registration error (display error message)
          this.toastr.error('Registration failed. Please try again.', error.error);
          console.error('Registration failed:', error.error);
        });
  }
}