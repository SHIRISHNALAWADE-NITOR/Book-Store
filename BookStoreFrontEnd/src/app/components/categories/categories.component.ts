import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BookService } from 'src/app/services/book.service'; // Adjust path if necessary
import { Book } from 'src/app/services/book.model'; // Adjust path if necessary

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: string[] = []; // Categories as a string array
  books: Book[] = [];
  currentBooks: Book[] = [];
  currentPage = 1;
  totalPages = 1;
  selectedCategory: string = '';
  stars: number[] = [1, 2, 3, 4, 5]; // Array for star rating

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private bookService: BookService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.selectedCategory = params['category'] || '';
      this.fetchCategories();
      this.fetchBooks();
    });
  }

  fetchCategories(): void {
    this.bookService.getBooks().subscribe(
      (data) => {
        this.categories = Array.from(new Set(data.map(book => book.category).filter((c): c is string => c !== undefined)));
        console.log('Categories fetched successfully:', this.categories);
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  fetchBooks(): void {
    this.bookService.getBooksByCategory(this.selectedCategory).subscribe(
      (data) => {
        this.books = data;
        this.updateCurrentBooks();
      },
      (error) => {
        console.error('Error fetching books:', error);
      }
    );
  }

  updateCurrentBooks(): void {
    const itemsPerPage = 10; // Adjust as needed
    this.totalPages = Math.ceil(this.books.length / itemsPerPage);
    this.currentBooks = this.books.slice((this.currentPage - 1) * itemsPerPage, this.currentPage * itemsPerPage);
  }

  onCategoryChange(event: Event): void {
    const target = event.target as HTMLSelectElement; // Cast to HTMLSelectElement
    const selectedCategory = target.value;
    this.selectedCategory = selectedCategory;
    this.fetchBooks();
  }
  

  prevPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateCurrentBooks();
    }
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateCurrentBooks();
    }
  }

  navigateToBook(bookId: number): void {
    this.router.navigate(['/home/book', bookId]);
  }

  roundedRating(rating: number | undefined): number {
    return Math.round(rating ?? 0); // Handle undefined ratings
  }
}
