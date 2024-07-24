import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  isLoggedIn: boolean = false;

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    const apiUrl = 'http://localhost:5134/api/auth/authenticate';
    const body = {
      username: this.username,
      password: this.password
    };

    this.http.post(apiUrl, body).subscribe(
      (response: any) => {
        // Handle successful authentication here (e.g., store token in localStorage)
        console.log('Authentication successful');
        console.log('Token:', response.token);

        // Store token in localStorage (example)
        localStorage.setItem('token', response.token);

        // Set isLoggedIn flag to true
        this.isLoggedIn = true;

        // Redirect to home page
        this.router.navigate(['/home']);
      },
      (error) => {
        // Handle authentication error (e.g., display error message)
        console.error('Authentication error:', error);
      }
    );
  }

  logout() {
    // Clear localStorage or perform logout actions
    localStorage.removeItem('token');
    this.isLoggedIn = false;
  }
}
