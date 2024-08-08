// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { Book } from 'src/app/services/book.model'; // Import your book model
// import { BookService } from 'src/app/services/book.service'; // Import your book service

// @Component({
//   selector: 'app-inventory',
//   templateUrl: './inventory.component.html',
//   styleUrls: ['./inventory.component.css']
// })
// export class InventoryComponent implements OnInit {

//   categories: string[] = [
//     'Adventure stories',
//     'Literary Collections',
//     'Philosophy',
//     'Language Arts & Disciplines',
//     'History',
//     'Business & Economics',
//     'Literary Criticism',
//     'Science',
//     'Juvenile Nonfiction',
//     'Poetry',
//     'Psychology',
//     'Religion',
//     'Social Science',
//     'Art',
//     'Drama',
//     'Performing Arts',
//     'Biography & Autobiography',
//     'Fiction'
//   ];
//   books: Book[] = [];
//   selectedBook: Book = {} as Book; // Default value for book
//   currentPage: number = 1;
//   itemsPerPage: number = 10;
//   adding: boolean = false;
//   isEditing: boolean = false;
//   selectedCategory: string | null = null;

//   constructor(private bookService: BookService,private router:Router) { }

//   ngOnInit(): void {
//     this.fetchBooks();
//   }

  

//   onCategoryChange(event: Event): void {
//     const target = event.target as HTMLSelectElement;
//     this.selectedCategory = target.value;
//     console.log('Selected Category:', this.selectedCategory);
//   }
//   fetchBooks(): void {
//     this.bookService.getBooks().subscribe(
//       data => {
//         this.books = data;
//       },
//       error => {
//         console.error('Error fetching books:', error);
//       }
//     );
//   }

//   // Pagination logic
//   nextPage(): void {
//     if (this.currentPage < this.totalPages) {
//       this.currentPage++;
//     }
//   }

//   prevPage(): void {
//     if (this.currentPage > 1) {
//       this.currentPage--;
//     }
//   }

//   get totalPages(): number {
//     return Math.ceil(this.books.length / this.itemsPerPage);
//   }

//   get currentBooks(): Book[] {
//     const startIndex = (this.currentPage - 1) * this.itemsPerPage;
//     return this.books.slice(startIndex, startIndex + this.itemsPerPage);
//   }

//   startAdding(): void {
//     this.selectedBook = {} as Book; // Reset selected book
//     this.adding = true;
//     this.isEditing = false;
//   }

//   editBook(book: Book): void {
//     this.selectedBook = { ...book };
//     this.adding = false;
//     this.isEditing = true;
//   }

//   saveBook(): void {
//     console.log('Saving book:', this.selectedBook);
//     if (this.adding) {
//       this.bookService.createBook(this.selectedBook).subscribe({
//         next: () => {
//           this.fetchBooks();
//           this.cancel();
//         },
//         error: (err) => {
//           console.error('Error adding book:', err);
//         }
//       });
//     } else if (this.isEditing) {
//       this.bookService.updateBook(this.selectedBook).subscribe({
//         next: () => {
//           this.fetchBooks();
//           this.cancel();
//         },
//         error: (err) => {
//           console.error('Error updating book:', err);
//         }
//       });
//     }
//   }
  

//   deleteBook(id: number): void {
//     if (confirm('Are you sure you want to delete this book?')) {
//       this.bookService.deleteBook(id).subscribe({
//         next: () => {
//           this.fetchBooks();
//         },
//         error: (err) => {
//           console.error('Error deleting book:', err);
//         }
//       });
//     }
//   }

//   cancel(): void {
//     this.selectedBook = {} as Book;
//     this.adding = false;
//     this.isEditing = false;
//   }

//   addfiles(id:number){
//     this.router.navigate([`/home/fileform/${id}`])
//   }
// }


import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Book } from 'src/app/services/book.model';
import { BookService } from 'src/app/services/book.service';
import { BookFormDialogComponent } from '../book-form-dialog/book-form-dialog.component'; // Import your dialog component

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
  selectedBook: Book = {} as Book; // Default value for book
  currentPage: number = 1;
  itemsPerPage: number = 10;
  adding: boolean = false;
  isEditing: boolean = false;
  selectedCategory: string | null = null;

  constructor(
    private bookService: BookService,
    private router: Router,
    public dialog: MatDialog // Inject MatDialog
  ) { }

  ngOnInit(): void {
    this.fetchBooks();
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
    const dialogRef = this.dialog.open(BookFormDialogComponent, {
      width: '500px',
      data: { book: {} as Book, adding: true, categories: this.categories }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.fetchBooks(); // Reload books after adding
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
        this.fetchBooks(); // Reload books after updating
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
