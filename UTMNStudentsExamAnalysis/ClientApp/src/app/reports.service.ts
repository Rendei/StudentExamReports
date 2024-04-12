import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { School } from './report-generator/models/school';
import { Class } from './report-generator/models/class';
import { TestType } from './report-generator/models/testType';
import { Subject } from './report-generator/models/subject';
import { SchoolAverage, ClassAverage } from './report-generator/models/averages';


@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private apiUrl = 'https://localhost:7230/api';
  constructor(private http: HttpClient) { }

  getSchools(): Observable<School[]> {
    return this.http.get<School[]>(`${this.apiUrl}/schools`);
  }

  //getClasses(): Observable<Class[]> {
  //  return this.http.get<Class[]>(`${this.apiUrl}/classes`);
  //}

  getTestTypes(): Observable<TestType[]> {
    return this.http.get<TestType[]>(`${this.apiUrl}/test-types`);
  }

  getSubjects(): Observable<Subject[]> {
    return this.http.get<Subject[]>(`${this.apiUrl}/subjects`);
  }

  getYears(): Observable<number[]> {
    return this.http.get<number[]>(`${this.apiUrl}/testtemplates/years`);
  }

  getSchoolClasses(schoolCode: number): Observable<Class[]> {
    return this.http.get<Class[]>(`${this.apiUrl}/results/classes/${schoolCode}`);
  }

  getSchoolsAverage(selectedSchoolCodes: number[], selectedSubjects: number[], seletectedYears: number[]): Observable<SchoolAverage[]> {
    const subjectsString = selectedSubjects.join('&selectedSubjects=');
    const schoolCodesString = selectedSchoolCodes.join('&selectedSchoolCodes=');    
    const yearsString = seletectedYears.join('&selectedYears=');

    return this.http.get<SchoolAverage[]>(`${this.apiUrl}/results/schools/average?selectedSchoolCodes=${schoolCodesString}&selectedSubjects=${subjectsString}&selectedYears=${yearsString}`);
  }

  getSchoolClassesAverage(schoolCode: number, selectedSchoolClasses: string[]): Observable<ClassAverage[]> {
    const schoolClassesString = selectedSchoolClasses.join('&selectedSchoolClasses=');

    return this.http.get<ClassAverage[]>(`${this.apiUrl}/results/classes/${schoolCode}/average?selectedSchoolClasses=${schoolClassesString}`);
  }
}

