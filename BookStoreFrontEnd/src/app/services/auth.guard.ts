import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(): boolean {
    const adminId = localStorage.getItem('roleId');

    // Allow access only if adminId is '1'
    if (adminId === '1') {
      return true; // Allow access
    } else {
      // Redirect to home or another page if adminId is not '1'
      this.router.navigate(['/home']);
      return false;
    }
  }
}
