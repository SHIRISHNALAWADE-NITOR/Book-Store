import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  roleId: number | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.isLoggedIn$.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
      if (this.isLoggedIn) {
        this.updateRoleId();
      } else {
        this.roleId = null;
      }
    });
  }

  private updateRoleId() {
    const roleIdFromStorage = localStorage.getItem('roleId');
    this.roleId = roleIdFromStorage ? parseInt(roleIdFromStorage, 10) : null;
  }

  navigateToLogin() {
    this.router.navigate(['home/login']);
  }

  logout() {
    this.authService.logout();
    this.roleId = null; // Reset roleId in the component
  }

  manageBooks(){
    this.router.navigate(['/home/inventory'])
  }
}
