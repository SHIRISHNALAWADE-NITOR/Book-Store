<template>
  <div>
    <CarouselComponent/>
    <h1>Categories</h1>
  </div>
</template>

<script>
import CarouselComponent from '@/components/CarouselComponent.vue';

export default {
  name: 'HomeView',
  components: {
    CarouselComponent
  }
};
</script>

<style scoped>
/* Scoped styles for HomeView component */
</style>
