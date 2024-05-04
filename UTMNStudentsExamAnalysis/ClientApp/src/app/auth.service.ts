import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

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
        console.log(token);
        this.setToken(token);
        this.setUserFromToken(token);
        console.log(this.userSubject);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.userSubject.next(null);
  }

  register(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, credentials)
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

  getUser(): Observable<any> {
    return this.userSubject.asObservable();
  }

  getUserRole(): string {
    const user = this.userSubject.getValue();
    console.log(user);
    return user ? user.role : '';
  }

}

