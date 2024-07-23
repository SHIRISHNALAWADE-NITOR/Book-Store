import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor() {}

  onSubmit(): void {
    console.log(`Username: ${this.username}, Password: ${this.password}`);
    // Here you can implement further logic, e.g., validation, authentication
  }
}
