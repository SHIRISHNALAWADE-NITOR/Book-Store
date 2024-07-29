import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserDTO } from 'src/app/services/user.dto';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  formData: UserDTO = new UserDTO();
  confirmPassword: string = '';
  errorMessage: string | null = null;

  constructor(private http: HttpClient, private router: Router) {}

  submitForm(): void {
    if (this.validateForm()) {
      // Map username to name
      this.formData.name = this.formData.username;
      this.formData.userId = 0; // Ensure userId is set
      this.formData.roleId = 2; // Ensure roleId is set

      this.http.post('http://localhost:5134/api/auth/Register', this.formData)
        .subscribe(
          response => {
            console.log('User registration successful:', response);
            this.resetForm();
            this.router.navigate(['/home']);
          },
          error => {
            console.error('Error registering user:', error);
            this.errorMessage = 'An error occurred while registering the user. Please try again.';
          }
        );
    }
  }

  validateForm(): boolean {
    if (this.formData.passwordHash !== this.confirmPassword) {
      alert('Passwords do not match');
      return false;
    }
    return true;
  }

  resetForm(): void {
    this.formData = new UserDTO();
    this.confirmPassword = '';
    this.errorMessage = null;
  }
}
