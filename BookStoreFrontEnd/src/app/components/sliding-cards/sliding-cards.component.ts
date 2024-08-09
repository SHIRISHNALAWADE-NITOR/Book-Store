import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sliding-cards',
  templateUrl: './sliding-cards.component.html',
  styleUrls: ['./sliding-cards.component.css']
})
export class SlidingCardsComponent implements OnInit {
  categories: any[] = [];

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.fetchCategories();
  }

  async fetchCategories() {
    try {
      const response = await fetch('http://localhost:5134/api/Book/category/topbooks');
      const data = await response.json();
      console.log('Fetched data:', data);
      this.categories = data;
    } catch (error) {
      console.error('Error fetching categories:', error);
    }
  }

  handleClick(category: any) {
    this.router.navigate(['home/categories'], { queryParams: { category: category.category } });
  }
}
