import { Component } from '@angular/core';
import { UserDTO } from 'src/app/services/user.dto';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  formData: UserDTO = new UserDTO();
  confirmPassword: string = '';

  constructor(private http: HttpClient,private router:Router) {}

  submitForm(): void {
    if (this.validateForm()) {
      const userData = {
        username: this.formData.username,
        email: this.formData.email,
        roleId:this.formData.roleId,
        passwordHash: this.formData.password
      };

      // Replace 'http://your-backend-api-url/register' with your actual backend API endpoint
      this.http.post('http://localhost:5134/api/Users', userData)
        .subscribe(
          response => {
            console.log('User registration successful:', response);
            this.resetForm();
            this.router.navigate(['/home']);

          },
          error => {
            console.error('Error registering user:', error);
            // Handle error accordingly
          }
        );
    }
  }

  validateForm(): boolean {
    if (this.formData.password !== this.confirmPassword) {
      alert('Passwords do not match');
      return false;
    }
    return true;
  }

  resetForm(): void {
    this.formData.username = '';
    this.formData.email = '';
    this.formData.password = '';
    this.confirmPassword = '';
  }
}
