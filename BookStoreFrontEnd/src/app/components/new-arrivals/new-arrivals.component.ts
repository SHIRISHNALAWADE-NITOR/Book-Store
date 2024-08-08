import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-arrivals',
  templateUrl: './new-arrivals.component.html',
  styleUrls: ['./new-arrivals.component.css']
})
export class NewArrivalsComponent implements OnInit {
  books: any[] = [];

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.fetchBooks();
  }

  async fetchBooks() {
    try {
      const response = await fetch('http://localhost:5134/api/Book/year/2024');
      const data = await response.json();
      console.log('Fetched data:', data);
      this.books = data;
    } catch (error) {
      console.error('Error fetching books:', error);
    }
  }

  handleClick(book: any) {
    this.router.navigate(['/home/individualBook', book.bookId.toString()]);
  }
}
