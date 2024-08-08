import { Component, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss']
})
export class CarouselComponent implements OnInit, OnDestroy {

  // Static data for the carousel slides
  carouselSlides = [
    { image: '/assets/images/baner/79_inr.jpg', caption: 'Fiction Books' },
    { image: '/assets/images/baner/80_inr.jpg', caption: 'Special Offers' },
    { image: '/assets/images/baner/81_inr.jpg', caption: 'Special Offers' },
    { image: '/assets/images/baner/83_inr.jpg', caption: 'Special Offers' },
    { image: '/assets/images/baner/84_inr.jpg', caption: 'Special Offers' },
    { image: '/assets/images/baner/85_inr.jpg', caption: 'Special Offers' },
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
