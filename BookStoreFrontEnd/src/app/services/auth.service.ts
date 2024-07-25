import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loggedInSubject: BehaviorSubject<boolean>;
  public isLoggedIn$: Observable<boolean>;

  constructor(private http: HttpClient, private router: Router) {
    this.loggedInSubject = new BehaviorSubject<boolean>(false);
    this.isLoggedIn$ = this.loggedInSubject.asObservable();
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

  login(username: string, password: string): Observable<any> {
    const apiUrl = 'http://localhost:5134/api/auth/authenticate';
    const body = { username, password };

    return this.http.post(apiUrl, body).pipe(
      tap((response: any) => {
        console.log(response.roleId);
        localStorage.setItem('token', response.token);
        localStorage.setItem('roleId', response.roleId);
        this.loggedInSubject.next(true);
        this.router.navigate(['/home']); // Navigate to home page after successful login
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('roleId');
    this.loggedInSubject.next(false);
    this.router.navigate(['home']); // Navigate to login page after logout
  }

  isAuthenticated(): boolean {
    return this.loggedInSubject.value;
  }
}
