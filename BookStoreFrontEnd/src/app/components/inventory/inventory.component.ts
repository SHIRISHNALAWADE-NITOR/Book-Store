import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/services/book.model'; // Import your book model
import { BookService } from 'src/app/services/book.service'; // Import your book service

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  books: Book[] = [];
  selectedBook: Book = {} as Book; // Default value for book
  currentPage: number = 1;
  itemsPerPage: number = 10;
  adding: boolean = false;
  isEditing: boolean = false;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.fetchBooks();
  }

  fetchBooks(): void {
    this.bookService.getBooks().subscribe(
      data => {
        this.books = data;
      },
      error => {
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

  get totalPages(): number {
    return Math.ceil(this.books.length / this.itemsPerPage);
  }

  get currentBooks(): Book[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.books.slice(startIndex, startIndex + this.itemsPerPage);
  }

  startAdding(): void {
    this.selectedBook = {} as Book; // Reset selected book
    this.adding = true;
    this.isEditing = false;
  }

  editBook(book: Book): void {
    this.selectedBook = { ...book };
    this.adding = false;
    this.isEditing = true;
  }

  saveBook(): void {
    if (this.adding) {
      this.bookService.createBook(this.selectedBook).subscribe({
        next: () => {
          this.fetchBooks();
          this.cancel();
        },
        error: (err) => {
          console.error('Error adding book:', err);
        }
      });
    } else if (this.isEditing) {
      this.bookService.updateBook(this.selectedBook).subscribe({
        next: () => {
          this.fetchBooks();
          this.cancel();
        },
        error: (err) => {
          console.error('Error updating book:', err);
        }
      });
    }
  }

  deleteBook(id: number): void {
    if (confirm('Are you sure you want to delete this book?')) {
      this.bookService.deleteBook(id).subscribe({
        next: () => {
          this.fetchBooks();
        },
        error: (err) => {
          console.error('Error deleting book:', err);
        }
      });
    }
  }

  cancel(): void {
    this.selectedBook = {} as Book;
    this.adding = false;
    this.isEditing = false;
  }
}
