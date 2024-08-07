// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { CartItem } from 'src/app/services/cart-item.model'; // Ensure this path is correct

// @Injectable({
//   providedIn: 'root'
// })
// export class CartService {
//   private apiUrl = 'http://localhost:5134/api/CartItem'; // Adjust API base URL as needed

//   constructor(private http: HttpClient) { }

//   // Get all cart items for the current user
//   getAllCartItems(): Observable<CartItem[]> {
//     return this.http.get<CartItem[]>(this.apiUrl);
//   }

//   // Get a specific cart item by ID
//   getCartItemById(id: number): Observable<CartItem> {
//     return this.http.get<CartItem>(`${this.apiUrl}/${id}`);
//   }

//   // Add a new cart item
//   addCartItem(cartItem: CartItem): Observable<CartItem> {
//     return this.http.post<CartItem>(this.apiUrl, cartItem);
//   }

//   // Update an existing cart item
//   updateCartItem(id: number, cartItem: CartItem): Observable<void> {
//     return this.http.put<void>(`${this.apiUrl}/${id}`, cartItem);
//   }

//   // Delete a cart item by ID
//   deleteCartItem(id: number): Observable<void> {
//     return this.http.delete<void>(`${this.apiUrl}/${id}`);
//   }
// }


import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';
import { CartItem } from 'src/app/services/cart-item.model';
import { Book } from 'src/app/services/book.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  
  private apiUrl = 'http://localhost:5134/api/CartItem';
 // private cartApiUrl = ``;
  private bookApiUrl = 'http://localhost:5134/api/Book';

  private cartSubject = new BehaviorSubject<CartItem[]>([]);
  private totalAmountSubject = new BehaviorSubject<number>(0);
  private totalItemsSubject = new BehaviorSubject<number>(0);

  constructor(private http: HttpClient) {
    this.loadCart();
  }

 // Adjust the method to handle Observable correctly
 public loadCart(): void {
  const userId=localStorage.getItem("userId")
  console.log(userId)
  if (userId) {
    this.http.get<CartItem[]>(`http://localhost:5134/api/CartItem/user/${userId}`).subscribe({
      next: cartItems => {
        // Fetch book details for each cart item
        const cartItemsWithBooks$ = this.fetchBooksForCartItems(cartItems);
        cartItemsWithBooks$.subscribe({
          next: itemsWithBooks => {
            this.cartSubject.next(itemsWithBooks);
            this.updateTotals(itemsWithBooks);
          },
          error: err => console.error('Error fetching books:', err)
        });
      },
      error: err => console.error('Error fetching cart items:', err)
    });
  } else {
    console.error('User ID is not available.');
  }
}

private fetchBooksForCartItems(cartItems: CartItem[]): Observable<CartItem[]> {
  const bookRequests = cartItems.map(item =>
    this.http.get<Book>(`${this.bookApiUrl}/${item.bookId}`).pipe(
      map(book => ({ ...item, book }))
    )
  );
  return forkJoin(bookRequests);
}

private updateTotals(cartItems: CartItem[]): void {
  const totalAmount = Math.round(
    cartItems.reduce((sum, item) => sum + (item.quantity * (item.book?.price || 0)), 0)
  );
  const totalItems = cartItems.reduce((sum, item) => sum + item.quantity, 0);

  this.totalAmountSubject.next(totalAmount);
  this.totalItemsSubject.next(totalItems);
}

  getCart(): Observable<CartItem[]> {
    return this.cartSubject.asObservable();
  }

  getTotalAmount(): Observable<number> {
    return this.totalAmountSubject.asObservable();
  }

  getTotalItems(): Observable<number> {
    return this.totalItemsSubject.asObservable();
  }

  addCartItem(cartItem: Omit<CartItem, 'cartItemId'>): Observable<CartItem> {
    return this.http.post<CartItem>(this.apiUrl, cartItem).pipe(
      map((createdCartItem) => {
        this.loadCart();
        return createdCartItem;
      })
    );
  }
  

  updateCartItem(id: number, cartItem: CartItem): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, cartItem).pipe(
      map(() => {
        this.loadCart();
      })
    );
  }

  deleteCartItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      map(() => {
        this.loadCart();
      })
    );
  }

  getBookById(bookId: number): Observable<Book> {
    return this.http.get<Book>(`${this.bookApiUrl}/${bookId}`);
  }
  clearCart(): Observable<void> {
    const userId = localStorage.getItem("userId");
    if (userId) {
      return this.http.delete<void>(`${this.apiUrl}/user/${userId}`).pipe(
        map(() => {
          this.cartSubject.next([]);
          this.totalAmountSubject.next(0);
          this.totalItemsSubject.next(0);
        }),
        // Error handling can be added here if needed
      );
    } else {
      throw new Error('User ID is not available.');
    }
  }
 
}
