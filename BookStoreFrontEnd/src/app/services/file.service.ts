
// file.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
 
@Injectable({
  providedIn: 'root'
})
export class FileService {
  private apiUrl = 'http://localhost:5134/api/files';
 
  constructor(private http: HttpClient) {}
 
  uploadFiles(formData: FormData): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/upload`, formData, {
      headers: new HttpHeaders({
        'Accept': 'application/json'
      })
    });
  }
 
 
  getAudioFile(bookId: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/audio/${bookId}`, { responseType: 'blob' });
  }
 
  getVideoFile(bookId: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/video/${bookId}`, { responseType: 'blob' });
  }
 
  getPdfFile(bookId: number): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/pdf/${bookId}`, { responseType: 'blob' });
  }
 
  deleteFile(fileId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${fileId}`);
  }
 
 
}
 
 