import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { School } from './report-generator/models/school';
import { Class } from './report-generator/models/class';
import { TestType } from './report-generator/models/testType';
import { Subject } from './report-generator/models/subject';
import { SchoolAverage } from './report-generator/models/schoolAverage';


@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private apiUrl = 'https://localhost:7230/api';
  constructor(private http: HttpClient) { }

  getSchools(): Observable<School[]> {
    return this.http.get<School[]>(`${this.apiUrl}/schools`);
  }

  getClasses(): Observable<Class[]> {
    return this.http.get<Class[]>(`${this.apiUrl}/classes`);
  }

  getTestTypes(): Observable<TestType[]> {
    return this.http.get<TestType[]>(`${this.apiUrl}/test-types`);
  }

  getSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${this.apiUrl}/subjects`);
  }

  getSchoolsAverage(schoolCodes: number[]): Observable<SchoolAverage[]> {
    console.log(schoolCodes);
    const schoolCodesString = schoolCodes.join('&schoolCodes=');
    console.log(schoolCodesString);
      
    return this.http.get<SchoolAverage[]>(`${this.apiUrl}/results/average?schoolCodes=${schoolCodesString}`);
  }
}

