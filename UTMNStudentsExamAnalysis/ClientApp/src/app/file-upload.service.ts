import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {

  private apiUrl = 'https://localhost:7230/api/files';
  constructor(private http: HttpClient) { }

  uploadFile(formData: FormData) {
    return this.http.post(`${this.apiUrl}/upload`, formData);
  }
}
