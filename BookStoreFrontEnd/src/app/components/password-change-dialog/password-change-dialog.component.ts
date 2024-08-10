import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change-dialog.component.html',
  styleUrls: ['./password-change-dialog.component.css']
})
export class PasswordChangeDialogComponent implements OnInit {
  email: string = '';
  dateOfBirth: string = '';
  token: string = '';
  otp:string='';
  newPassword: string = '';

  private resetPasswordUrl = 'http://localhost:5134/api/Auth/ResetPasswordEmail';

  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Extract query parameters from the URL
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
      this.dateOfBirth = params['dateOfBirth'];
      this.token = params['token'];
    });
  }

  onSubmit() {
    if (!this.email || !this.dateOfBirth || !this.token || !this.newPassword || !this.otp) {
      console.error('All fields are required.');
      return;
    }

    const resetPayload = {
      email: this.email,
      dateOfBirth: this.dateOfBirth,
      token: this.token,
      otp:this.otp,
      newPassword: this.newPassword
    };

    this.http.post(this.resetPasswordUrl, resetPayload, {
      headers: { 'Content-Type': 'application/json' },
      responseType: 'text' // Specify that the response type is text
    }).subscribe({
      next: (response) => {
        // Handle success
        console.log('Password reset successful:', response);
        this.router.navigate(['/home/login']);
      },
      error: (error) => {
        console.error('Password reset error:', error);
      }
    });
    
  }

  onCancel() {
    this.router.navigate(['/']);
  }
}
