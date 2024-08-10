import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ToastService } from 'src/app/services/toast.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  roleId: number = 0; // Define roleId variable to store role from backend (assuming default is 0 for unknown)
  isLoggedIn: boolean = false;

  constructor(private authService: AuthService, private router: Router,private toastr:ToastService) {}

  onSubmit() {
    this.authService.login(this.email, this.password).subscribe(
      (response: any) => {
        this.isLoggedIn = true;
        localStorage.setItem("roleId", response.roleId.toString())
        this.roleId = response.roleId; // Store roleId received from backend
        this.toastr.show("Successfully loggedIn",'success')
      },
      (error) => {
        console.error('Login failed:', error);
      }
    );
  }

  logout() {

    this.authService.logout();
    this.toastr.show("Logged out",'success')
    this.isLoggedIn = false;
    this.roleId=0;
  }
}
