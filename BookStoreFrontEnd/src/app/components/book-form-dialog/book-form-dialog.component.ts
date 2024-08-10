import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';  // Correct imports
import { Book } from 'src/app/services/book.model';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-book-form-dialog',
  templateUrl: './book-form-dialog.component.html',
  styleUrls: ['./book-form-dialog.component.css']
})
export class BookFormDialogComponent {
  categories: string[] = [
    'Adventure stories', 'Literary Collections', 'Philosophy', 'Language Arts & Disciplines',
    'History', 'Business & Economics', 'Literary Criticism', 'Science', 'Juvenile Nonfiction',
    'Poetry', 'Psychology', 'Religion', 'Social Science', 'Art', 'Drama', 'Performing Arts',
    'Biography & Autobiography', 'Fiction'
  ];

  constructor(
    public dialogRef: MatDialogRef<BookFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { book: Book, adding: boolean },
    private bookService: BookService
  ) { }

  saveBook(): void {
    if (this.data.adding) {
      this.bookService.createBook(this.data.book).subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => console.error('Error adding book:', err)
      });
    } else {
      this.bookService.updateBook(this.data.book).subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => console.error('Error updating book:', err)
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
