import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TownType } from './town-type';

@Injectable({
  providedIn: 'root'
})
export class TownTypeService {
  private apiUrl = 'https://localhost:7230/api';

  constructor(private http: HttpClient) { }

  getTownTypes(): Observable<TownType[]> {
    return this.http.get<TownType[]>(`${this.apiUrl}/towntypes`);
  }
}
