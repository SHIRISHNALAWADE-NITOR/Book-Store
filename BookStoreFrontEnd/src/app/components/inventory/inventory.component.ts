import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Book } from 'src/app/services/book.model';
import { BookService } from 'src/app/services/book.service';
import { BookFormDialogComponent } from '../book-form-dialog/book-form-dialog.component';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  categories: string[] = [
    'Adventure stories', 'Literary Collections', 'Philosophy', 'Language Arts & Disciplines',
    'History', 'Business & Economics', 'Literary Criticism', 'Science', 'Juvenile Nonfiction',
    'Poetry', 'Psychology', 'Religion', 'Social Science', 'Art', 'Drama', 'Performing Arts',
    'Biography & Autobiography', 'Fiction'
  ];

  books: Book[] = [];
  selectedBook: Book = {} as Book;
  filteredBooks: Book[] = [];
  currentPage: number = 1;
  itemsPerPage: number = 10;
  searchQuery: string = '';
  adding: boolean = false;
  isEditing: boolean = false;
  selectedCategory: string | null = null;

  constructor(
    private bookService: BookService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.fetchBooks();
    this.route.queryParams.subscribe(params => {
      this.searchQuery = params['search'] || '';
      console.log('Search Query from URL:', this.searchQuery);
      this.currentPage = 1; // Reset to first page on new search
      this.filterBooks();
    });
  }
  

  onCategoryChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    this.selectedCategory = target.value;
    console.log('Selected Category:', this.selectedCategory);
  }

  fetchBooks(): void {
    this.bookService.getBooks().subscribe(
      data => {
        this.books = data;
        this.filterBooks(); // Apply filter after fetching
      },
      error => {
        console.error('Error fetching books:', error);
      }
    );
  }
  
  filterBooks(): void {
    if (this.searchQuery) {
      const query = this.searchQuery.toLowerCase();
      this.filteredBooks = this.books.filter(book =>
        book.title?.toLowerCase().includes(query) ||
        book.author?.toLowerCase().includes(query) 
      );
      console.log('Filtered Books:', this.filteredBooks); // Debugging line to check if filtering is working
    } else {
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

  get totalPages(): number {
    return Math.ceil(this.books.length / this.itemsPerPage);
  }

  get currentBooks(): Book[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredBooks.slice(startIndex, startIndex + this.itemsPerPage);
  }
  
  startAdding(): void {
    const dialogRef = this.dialog.open(BookFormDialogComponent, {
      width: '500px',
      data: { book: {} as Book, adding: true, categories: this.categories }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchBooks();
      }
    });
  }

  editBook(book: Book): void {
    const dialogRef = this.dialog.open(BookFormDialogComponent, {
      width: '500px',
      data: { book: { ...book }, adding: false, categories: this.categories }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchBooks();
      }
    });
  }

  deleteBook(id: number): void {
    if (id) {
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

  addfiles(id: number): void {
    this.router.navigate([`/home/fileform/${id}`]);
  }

  saveBook(): void {
    console.log('Saving book:', this.selectedBook);
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
    }
  }
}
