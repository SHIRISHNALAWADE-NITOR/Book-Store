import { Component, OnInit, OnDestroy } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { CartItem } from 'src/app/services/cart-item.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit, OnDestroy {
  cartItems: CartItem[] = [];
  totalAmount: number = 0;
  totalItems: number = 0;

  private routerSub: Subscription | null = null;
  private cartSub: Subscription | null = null;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.cartService.loadCart();
    this.loadCartData();
  }

  private loadCartData(): void {
    if (this.cartSub) {
      this.cartSub.unsubscribe();
    }
    this.cartSub = this.cartService.getCart().subscribe(cartItems => {
      console.log('Cart items in component:', cartItems); // Debug log
      this.cartItems = cartItems;
      this.updateTotals();
    });
    this.cartService.getTotalAmount().subscribe(amount => this.totalAmount = amount);
    this.cartService.getTotalItems().subscribe(items => this.totalItems = items);
  }

  updateTotals(): void {
    this.totalAmount = this.cartItems.reduce((sum, item) => sum + (item.quantity * (item.book?.price || 0)), 0);
    this.totalItems = this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
  }

  removeFromCart(cartItemId: number): void {
    this.cartService.deleteCartItem(cartItemId).subscribe(() => {
      this.cartItems = this.cartItems.filter(item => item.cartItemId !== cartItemId);
      this.updateTotals();
    });
  }

  checkout(): void {
    if (this.authService.isAuthenticated() && this.cartItems.length > 0) {
      this.router.navigate(["home/payment"]);
    } else if (this.cartItems.length <= 0) {
      this.router.navigate(['home']);
    } else {
      this.router.navigate(['home/login']);
    }
  }

  adjustQuantity(cartItemId: number, change: number): void {
    const cartItem = this.cartItems.find(item => item.cartItemId === cartItemId);
    if (cartItem) {
      const newQuantity = cartItem.quantity + change;
      if (newQuantity <= 0) {
        this.removeFromCart(cartItemId);
      } else {
        // Update quantity locally and in the backend
        cartItem.quantity = newQuantity;
        this.cartService.updateCartItem(cartItemId, { ...cartItem, quantity: newQuantity }).subscribe({
          next: () => {
            console.log(`Cart item ${cartItemId} updated`);
            this.updateTotals();
          },
          error: err => console.error('Error updating cart item:', err)
        });
      }
    }
  }

  onQuantityChange(cartItemId: number, newQuantity: number): void {
    if (newQuantity <= 0) {
      this.removeFromCart(cartItemId);
    } else {
      // Update quantity locally and in the backend
      const cartItem = this.cartItems.find(item => item.cartItemId === cartItemId);
      if (cartItem) {
        cartItem.quantity = newQuantity;
        this.cartService.updateCartItem(cartItemId, { ...cartItem, quantity: newQuantity }).subscribe({
          next: () => {
            console.log(`Cart item ${cartItemId} updated`);
            this.updateTotals();
          },
          error: err => console.error('Error updating cart item:', err)
        });
      }
    }
  }

  ngOnDestroy(): void {
    if (this.routerSub) {
      this.routerSub.unsubscribe();
    }
    if (this.cartSub) {
      this.cartSub.unsubscribe();
    }
  }
}
