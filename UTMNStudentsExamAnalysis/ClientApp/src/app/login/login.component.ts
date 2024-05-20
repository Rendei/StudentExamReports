import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { merge } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  email = new FormControl('', [Validators.required, Validators.email]);
  errorMessage = '';
  hide = true;

  constructor(private authService: AuthService, private router: Router, private toastr: ToastrService) {
    merge(this.email.statusChanges, this.email.valueChanges)      
      .subscribe(() => this.updateErrorMessage());
  }

  ngOnInit(): void {
  }

  login(email: string | null, password: string): void {
    let user = { email, password };    
    this.authService.login(user)
      .subscribe(response => {
        console.log('Login successful:', response.token);
        this.router.navigate(['/']);
      },
        error => {          
          this.toastr.error(error.error, "Ошибка логина");
        });
  }

  updateErrorMessage() {
    if (this.email.hasError('required')) {
      this.errorMessage = 'Нужно ввести email';
    } else if (this.email.hasError('email')) {
      this.errorMessage = 'Не является почтовым адресом';
    } else {
      this.errorMessage = '';
    }
  }
}
