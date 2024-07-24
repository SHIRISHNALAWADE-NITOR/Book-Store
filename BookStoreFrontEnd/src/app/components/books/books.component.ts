import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  books: any[] = []; // Array to hold fetched books
  currentPage: number = 1;
  itemsPerPage: number = 10; // Number of items per page (9 records)

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchBooks();
  }

  fetchBooks(): void {
    //const apiUrl = 'https://jsonplaceholder.typicode.com/albums'; // Replace with your API URL
    // const apiUrl='assets/books.json';
    const apiUrl='http://localhost:5134/api/Book'
    this.http.get<any[]>(apiUrl)
      .subscribe(
        (data) => {
          this.books = data;
          console.log(data)
          console.log('Books fetched successfully:', this.books);
        },
        (error) => {
          console.error('Error fetching books:', error);
        }
      );
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
    return Math.ceil(this.books.length / this.itemsPerPage);
  }

  // Calculate the current page items
  get currentBooks(): any[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.books.slice(startIndex, startIndex + this.itemsPerPage);
  }

}
