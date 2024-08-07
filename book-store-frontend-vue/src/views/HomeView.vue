<template>
  <div>
    <CarouselComponent/>
    <h1>Categories</h1>
    
    <SlidingCards/>
    <h1>New Arrivals</h1>
    
    <NewArrivals/>
    
  </div>
</template>

<script>
import CarouselComponent from '@/components/CarouselComponent.vue';
import SlidingCards from '@/components/SlidingCards.vue';
import NewArrivals from '@/components/NewArrivals.vue'; 


export default {
  name: 'HomeView',
  components: {
    CarouselComponent,
    SlidingCards,
    NewArrivals
  }
};
</script>

<style scoped>
</style>
