import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Behavior } from 'popper.js';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7230/api/auth';
  private tokenKey = 'authToken';
  private userSubject = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    // Check if token exists in storage on service initialization
    const token = localStorage.getItem(this.tokenKey);
    if (token) {
      this.setUserFromToken(token);
    }
  }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: any) => {
        const token = response.token.result;        
        this.setToken(token);
        this.setUserFromToken(token);        
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.userSubject.next(null);
  }

  private setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  private setUserFromToken(token: string): void {
    // Decode the token and extract user information
    const decodedToken = this.jwtHelper.decodeToken(token);;
    //console.log(decodedToken);
    this.userSubject.next(decodedToken);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.tokenKey);
    return !this.jwtHelper.isTokenExpired(token);
  }

  getUser(): BehaviorSubject<any> {
    return this.userSubject.value;
  }

  getUserRole(): string {
    const user = this.userSubject.getValue();    
    return user ? user.role : '';
  }

}

