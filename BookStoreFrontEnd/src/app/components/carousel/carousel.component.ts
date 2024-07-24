import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss']
})
export class CarouselComponent implements OnInit, OnDestroy {

  // Static data for the carousel slides
  carouselSlides = [
    { image: '/assets/images/baner/fictionBaner.png', caption: 'Fiction Books' },
    { image: '/assets/images/baner/offerBaner.png', caption: 'Special Offers' },
    { image: '/assets/images/baner/SummerSale.png', caption: 'Summer Sale' }
  ];

  currentSlideIndex: number = 0;
  intervalId?: any;

  ngOnInit(): void {
    if (this.carouselSlides.length > 0) {
      this.startAutoSlide();
    }
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  startAutoSlide(): void {
    this.intervalId = setInterval(() => {
      this.nextSlide();
    }, 3000); // Change slide every 3 seconds
  }

  nextSlide(): void {
    this.currentSlideIndex = (this.currentSlideIndex + 1) % this.carouselSlides.length;
  }

  prevSlide(): void {
    this.currentSlideIndex = (this.currentSlideIndex - 1 + this.carouselSlides.length) % this.carouselSlides.length;
  }

  goToSlide(index: number): void {
    this.currentSlideIndex = index;
  }
}
