import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms'; // Import NgForm for template-driven forms

@Component({
  selector: 'app-individual-book',
  templateUrl: './individual-book.component.html',
  styleUrls: ['./individual-book.component.css']
})
export class IndividualBookComponent implements OnInit {

  bookId: string | null = null;
  bookDetails: any; // Define type as per your API response
  quantity: number = 1;
  stars: number[] = [1, 2, 3, 4, 5]; // Array to represent star ratings
  averageRating: number = 0; // Average rating out of 5

  constructor(private route: ActivatedRoute, private http: HttpClient) { }

  ngOnInit(): void {
    this.bookId = this.route.snapshot.paramMap.get('id'); // Fetch bookId from route parameter
    if (this.bookId) {
      this.fetchBookDetails();
    }
    
  }

  fetchBookDetails(): void {
    const apiUrl = `http://localhost:5134/api/Book/${this.bookId}`;
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
      this.averageRating = this.bookDetails.rating / 100;
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
}
