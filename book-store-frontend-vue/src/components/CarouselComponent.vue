<template>
  <div class="carousel-container">
    <div class="carousel-slides" :style="{'transform': 'translateX(-' + (currentSlideIndex * 100) + '%)'}">
      <div class="carousel-slide" v-for="(slide, i) in carouselSlides" :key="i" :class="{'active': i === currentSlideIndex}">
        <img :src="require(`../../public/c${i + 1}.jpg`)" :alt="slide.caption">
        <div class="carousel-caption">{{ slide.caption }}</div>
      </div>
    </div>
    <button class="carousel-control prev" @click="prevSlide()">&#10094;</button>
    <button class="carousel-control next" @click="nextSlide()">&#10095;</button>
    <div class="carousel-indicators">
      <span v-for="(slide, i) in carouselSlides" :key="i" :class="{'active': i === currentSlideIndex}" @click="goToSlide(i)"></span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CarouselComponent',
  data() {
    return {
      carouselSlides: [
        { caption: '' },
        { caption: '' },
        { caption: '' },
        { caption: '' },
        { caption: '' },
      ],
      currentSlideIndex: 0,
      intervalId: null
    };
  },
  mounted() {
    if (this.carouselSlides.length > 0) {
      this.startAutoSlide();
    }
  },
  beforeUnmount() {
    clearInterval(this.intervalId);
  },
  methods: {
    startAutoSlide() {
      this.intervalId = setInterval(() => {
        this.nextSlide();
      }, 3000); 
    },
    nextSlide() {
      this.currentSlideIndex = (this.currentSlideIndex + 1) % this.carouselSlides.length;
    },
    prevSlide() {
      this.currentSlideIndex = (this.currentSlideIndex - 1 + this.carouselSlides.length) % this.carouselSlides.length;
    },
    goToSlide(index) {
      this.currentSlideIndex = index;
    }
  }
};
</script>

<style scoped>
.carousel-container {
  position: relative;
  overflow: hidden;
  width: 100%;
  max-width: 100%;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.carousel-slides {
  display: flex;
  transition: transform 0.5s ease;
  width: 100%;
}

.carousel-slide {
  min-width: 100%;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
}

.carousel-slide img {
  width: 100%;
  height: auto;
}

.carousel-caption {
  position: absolute;
  bottom: 10px;
  left: 20px;
  background: rgba(0, 0, 0, 0.5);
  color: #fff;
  padding: 10px;
  border-radius: 5px;
}

.carousel-control {
  position: absolute;
  top: 50%;
  width: 40px;
  height: 40px;
  background: rgba(0, 0, 0, 0.5);
  color: #fff;
  border: none;
  cursor: pointer;
  font-size: 24px;
  text-align: center;
  line-height: 40px;
  border-radius: 50%;
  transform: translateY(-50%);
  z-index: 1000;
}

.prev {
  left: 10px;
}

.next {
  right: 10px;
}

.carousel-indicators {
  position: absolute;
  bottom: 10px;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  gap: 5px;
}

.carousel-indicators span {
  width: 10px;
  height: 10px;
  background: #ddd;
  border-radius: 50%;
  cursor: pointer;
}

.carousel-indicators .active {
  background: #333;
}
</style>
