import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login(email: string, password: string): void {
    let user = { email, password };
    this.authService.login(user)
      .subscribe(response => {
        console.log('Login successful:', response.token);
        this.router.navigate(['/']);
      },
        error => {
          console.error('Login failed:', error);
        });
  }
}
