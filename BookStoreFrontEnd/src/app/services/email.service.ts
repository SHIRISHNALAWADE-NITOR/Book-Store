import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmailService {
  private sendGridUrl = 'https://api.sendgrid.com/v3/mail/send';

  constructor(private http: HttpClient) {}

  sendEmail(to: string, subject: string, content: string): Observable<any> {
    const emailData = {
      personalizations: [{
        to: [{ email: to }],
        subject: subject
      }],
      from: { email: 'your-email@example.com' },
      content: [{
        type: 'text/plain',
        value: content
      }]
    };

    const headers = {
      'Authorization': `Bearer YOUR_SENDGRID_API_KEY`,
      'Content-Type': 'application/json'
    };

    return this.http.post(this.sendGridUrl, emailData, { headers });
  }
}
