import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import {MatPaginatorModule} from '@angular/material/paginator';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css'],
  
})
export class BooksComponent implements OnInit {
  books: any[] = []; // Array to hold fetched books
  filteredBooks: any[] = []; // Array to hold filtered books
  currentPage: number = 1;
  itemsPerPage: number = 10; // Number of items per page (10 records)
  searchQuery: string = ''; // Property to hold search query
  stars: number[] = [1, 2, 3, 4, 5]; // Array to represent star ratings
  averageRating: number = 0; // Average rating out of 5
  
  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.fetchBooks();
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['search'] || '';
      this.filterBooks();
    });
  }

  fetchBooks(): void {
    const apiUrl = 'http://localhost:5134/api/Book';
    this.http.get<any[]>(apiUrl).subscribe(
      (data) => {
        this.books = data;
        this.filterBooks(); // Apply search filter after fetching
        console.log('Books fetched successfully:', this.books);
      },
      (error) => {
        console.error('Error fetching books:', error);
      }
    );
  }

  filterBooks(): void {
    if (this.searchQuery) {
      // Convert search query to lowercase for case-insensitive comparison
      const query = this.searchQuery.toLowerCase();
 
      // Filter books by title or author if the search query is present
      this.filteredBooks = this.books.filter(book =>
        book.title.toLowerCase().includes(query) ||
        book.author.toLowerCase().includes(query) 
      );
    } else {
      // If no search query, show all books
      this.filteredBooks = this.books;
    }
  }

  // Pagination logic
  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  // Calculate number of pages based on itemsPerPage
  get totalPages(): number {
    return Math.ceil(this.filteredBooks.length / this.itemsPerPage);
  }

  // Calculate the current page items
  get currentBooks(): any[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredBooks.slice(startIndex, startIndex + this.itemsPerPage);
  }

  navigateToBook(id: number): void {
    this.router.navigate(['/home/individualBook', id.toString()]);
  }

   // Method to round book rating
   roundedRating(rating: number): number {
    return Math.round(rating);
  }
}


