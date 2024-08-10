// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { HttpClient } from '@angular/common/http';
// import { CartService } from 'src/app/services/cart.service'; // Ensure the path is correct
// import { Book } from 'src/app/services/book.model'; // Import Book model
// import { ToastService } from 'src/app/services/toast.service';

// @Component({
//   selector: 'app-individual-book',
//   templateUrl: './individual-book.component.html',
//   styleUrls: ['./individual-book.component.css']
// })
// export class IndividualBookComponent implements OnInit {

//   userId: string | null = null;
//   bookDetails: Book | null = null; // Use Book model
//   quantity: number = 1;
//   stars: number[] = [1, 2, 3, 4, 5]; // Array to represent star ratings
//   averageRating: number = 0; // Average rating out of 5

//   constructor(
//     private route: ActivatedRoute,
//     private http: HttpClient,
//     private cartService: CartService, // Inject CartService
//     private toastService: ToastService
//   ) { }

//   ngOnInit(): void {
//     this.userId = this.route.snapshot.paramMap.get('id'); // Fetch bookId from route parameter
//     if (this.userId) {
//       this.fetchBookDetails();
//     }
//   }

//   fetchBookDetails(): void {
//     const apiUrl = `http://localhost:5134/api/Book/${this.userId}`; // Adjusted API URL
//     this.http.get<Book>(apiUrl)
//       .subscribe(
//         (data) => {
//           this.bookDetails = data;
//           this.calculateRating();
//           console.log('Book details fetched successfully:', this.bookDetails);
//         },
//         (error) => {
//           console.error('Error fetching book details:', error);
//         }
//       );
//   }

//   // Calculate the star rating based on a number
//   calculateRating(): void {
//     if (this.bookDetails && this.bookDetails.rating) {
//       this.averageRating = this.bookDetails.rating / 100; // Assuming rating is a percentage
//     }
//   }

//   // Quantity counter methods
//   incrementQuantity(): void {
//     this.quantity++;
//   }

//   decrementQuantity(): void {
//     if (this.quantity > 1) {
//       this.quantity--;
//     }
//   }

//   // Validate input to accept only numbers
//   isNumber(event: KeyboardEvent): boolean {
//     return /[0-9]/i.test(event.key);
//   }

//   addToCart(): void {
//     if (this.bookDetails) {
//       this.cartService.addCartItem(this.bookDetails, this.quantity); // Use CartService to add to cart
//       console.log('Added to cart:', this.bookDetails);
//       this.toastService.show("Added to cart", 'success');
//     }
//   }
// }


import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CartService } from 'src/app/services/cart.service'; // Ensure this path is correct
import { CartItem } from 'src/app/services/cart-item.model'; // Ensure this path is correct
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-individual-book',
  templateUrl: './individual-book.component.html',
  styleUrls: ['./individual-book.component.css']
})
export class IndividualBookComponent implements OnInit {

  userId: string | null = null;
  bookDetails: any; // Define type as per your API response
  quantity: number = 1;
  stars: number[] = [1, 2, 3, 4, 5]; // Array to represent star ratings
  averageRating: number = 0; // Average rating out of 5
  bookId: string | null= null;

  constructor(
    private route: ActivatedRoute,
    private router:Router,
    private http: HttpClient,
    private cartService: CartService, // Inject CartService
    private toastService: ToastService
  ) { }

  ngOnInit(): void {
    this.bookId = this.route.snapshot.paramMap.get('id');
    this.userId = localStorage.getItem("userId");
    
    console.log("ngOnInit - userId:", this.userId);
  
    if (this.bookId) {
      this.fetchBookDetails();
    }
  }
  

  fetchBookDetails(): void {
    const apiUrl = `http://localhost:5134/api/book/${this.bookId}`;
    this.http.get<any>(apiUrl)
      .subscribe(
        (data) => {
          this.bookDetails = data;
          this.calculateRating();
          console.log('Book details fetched successfully:', this.bookDetails);
        },
        (error) => {
          console.error('Error fetching book details:', error);
        }
      );
  }

  // Calculate the star rating based on a three-digit number
  calculateRating(): void {
    if (this.bookDetails && this.bookDetails.rating) {
      this.averageRating = Math.round(this.bookDetails.rating);
    }
  }

  // Quantity counter methods
  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  // Validate input to accept only numbers
  isNumber(event: KeyboardEvent): boolean {
    return /[0-9]/i.test(event.key);
  }

  addToCart(): void {
    console.log("Clicked Add to cart");
    console.log("bookDetails:", this.bookDetails);
    console.log("userId:", this.userId);
  
    if (this.bookDetails && this.userId) {
      const cartItem: Omit<CartItem, 'cartItemId'> = {
        userId: Number(this.userId),
        bookId: this.bookDetails.bookId,
        quantity: this.quantity
      };
      console.log("Cart item to be added:", cartItem);
      this.toastService.show("Added to cart",'success')
      this.cartService.addCartItem(cartItem).subscribe(
        () => {
          console.log('Added to cart:', cartItem);
          this.router.navigate(["/home/cart"])
          this.toastService.show('Added to cart', 'success');
        },
        (error) => {
          console.error('Error adding to cart:', error);
          this.toastService.show('Error adding to cart', 'error');
        }
      );
    } else {
      this.router.navigate(["/home/login"])
      console.error("bookDetails or userId is missing");
    }
  }
  
  
}
