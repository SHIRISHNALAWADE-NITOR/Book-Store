import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { CartService } from 'src/app/services/cart.service';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  searchVisible: boolean = true;
  isLoggedIn: boolean = false;
  roleId: number | null = null;
  searchQuery: string = '';  // Property to bind the search query

  constructor(
    private authService: AuthService,
    private router: Router,
    private cartService: CartService
  ) {}

  ngOnInit() {
    // Subscribe to authentication state changes
    this.authService.isLoggedIn$.subscribe((loggedIn: boolean) => {
      this.isLoggedIn = loggedIn;
      if (this.isLoggedIn) {
        this.updateRoleId();
      } else {
        this.roleId = null;
      }
    });

    // Subscribe to router events to update search visibility
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.updateVisibility();
    });
  }

  private updateRoleId() {
    const roleIdFromStorage = localStorage.getItem('roleId');
    this.roleId = roleIdFromStorage ? parseInt(roleIdFromStorage, 10) : null;
  }

  private updateVisibility(): void {
    // Check if the current route is '/home/login'
    this.searchVisible = this.router.url !== '/home/login' && this.router.url !== '/home/signup';
    
  }

  navigateToLogin() {
    this.router.navigate(['home/login']);
  }

  logout() {
    this.authService.logout();
    this.roleId = null; // Reset roleId in the component
  }

  manageBooks() {
    this.router.navigate(['/home/inventory']);
  }

  getBooks() {
    this.router.navigate(['/home']);
  }

  loadCartforuser() {
    this.router.navigate(['/home/cart']).then(() => {
      this.cartService.loadCart();
    });
  }

  onSearch() {
    if (this.searchQuery.trim()) {
      this.router.navigate(['/home/books'], { queryParams: { search: this.searchQuery } });
    } else {
      // Clear search results or handle empty search case
      this.router.navigate(['/home']);
    }
  }
}
