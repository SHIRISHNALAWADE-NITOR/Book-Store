import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

interface ForgotPasswordRequest {
  email: string;
  dateOfBirth: string;
}

interface ForgotPasswordResponse {
  email: string;
  dateOfBirth: string;
  token: string;
}

@Component({
  selector: 'app-passwordchange',
  templateUrl: './passwordchange.component.html',
  styleUrls: ['./passwordchange.component.css']
})
export class PasswordchangeComponent {
  private apiUrl = 'http://localhost:5134/api/Auth/ForgotPasswordEmail';

  email: string = '';
  dateOfBirth: string = '';

  constructor(
    private router: Router,
    private http: HttpClient
  ) {}

  handlesubmit() {
    const payload: ForgotPasswordRequest = {
      email: this.email,
      dateOfBirth: this.dateOfBirth
    };

    this.http.post<ForgotPasswordResponse>(this.apiUrl, payload).subscribe({
      next: (response) => {
        // Save token in a service or local storage
        // Example: localStorage.setItem('resetToken', response.token);

        // Navigate to the password reset page
        this.router.navigate(['home/password-reset'], {
          queryParams: {
            email: this.email,
            dateOfBirth: this.dateOfBirth,
            token: response.token
          }
        });
      },
      error: (error) => {
        console.error('Error:', error);
      }
    });
  }
}
