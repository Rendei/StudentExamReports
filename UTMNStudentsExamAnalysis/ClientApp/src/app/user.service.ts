import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7230/api/users';
  constructor(private http: HttpClient) { }

  addUser(userInfo: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, userInfo);
  }

}
