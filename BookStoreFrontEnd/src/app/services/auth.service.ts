import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ToastService } from './toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInSubject: BehaviorSubject<boolean>;
  public isLoggedIn$: Observable<boolean>;
  private token: string | null = null;

  constructor(private http: HttpClient, private router: Router,private toastr:ToastService) {
    this.loggedInSubject = new BehaviorSubject<boolean>(false);
    this.isLoggedIn$ = this.loggedInSubject.asObservable();
    this.token = localStorage.getItem('token');
    this.checkLoginStatus();
  }

  private checkLoginStatus() {
    const token = localStorage.getItem('token');
    this.loggedInSubject.next(!!token);
    if (token) {
      const roleIdFromStorage = localStorage.getItem('roleId');
      if (roleIdFromStorage) {
        // Update roleId in BehaviorSubject or manage as needed
        // Example: this.roleIdSubject.next(parseInt(roleIdFromStorage, 10));
      }
    }
  }

  login(email: string, password: string): Observable<any> {
    const apiUrl = 'http://localhost:5134/api/auth/Login';
    const body = { email, password };

    return this.http.post(apiUrl, body).pipe(
      tap((response: any) => {
        console.log(response.roleId);
        localStorage.setItem('token', response.token);
        localStorage.setItem('roleId', response.roleId);
        localStorage.setItem('userId',response.userId)
        this.loggedInSubject.next(true);
        this.router.navigate(['/home']); // Navigate to home page after successful login
      })
    );
  }

  logout() {
    localStorage.clear();
    localStorage.removeItem('token');
    localStorage.removeItem('roleId');
    localStorage.removeItem('username')
    this.loggedInSubject.next(false);
    this.toastr.show("Logged out",'success')
    this.router.navigate(['home']); // Navigate to login page after logout
  }

  isAuthenticated(): boolean {
    return this.loggedInSubject.value;
  }


  getToken(): string | null {
    return this.token;
  }

  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('token', token);
  }

  clearToken(): void {
    this.token = null;
    localStorage.removeItem('token');
  }

  getCurrentUserId(): string {
    // Example: Replace with actual user ID retrieval logic
    return localStorage.getItem('userId') || 'guest'; // Use localStorage or any method to retrieve the user ID
  }
  // isAuthenticated(): boolean {
  //   return !!localStorage.getItem('userId'); // Example check
  // }
}
