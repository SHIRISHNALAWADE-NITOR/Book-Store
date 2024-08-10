import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private apiUrl = 'http://localhost:5134/api/Email/send'; // Updated endpoint

  constructor(private http: HttpClient) {}

  sendEmail(emailToId: string, emailToName: string, emailSubject: string, emailBody: string): Observable<any> {
    // Construct the payload to match the new API structure
    const emailData = {
      emailToId: emailToId,
      emailToName: emailToName,
      emailSubject: emailSubject,
      emailBody: emailBody
    };

    // Set headers if needed; adjust as necessary
    const headers = {
      'Content-Type': 'application/json'
      // Add other headers if required, such as authentication headers
    };

    // Make the HTTP POST request
    return this.http.post(this.apiUrl, emailData, { headers });
  }
}
