<template>
    <div class="cart-view-container">
      <h1>Shopping Cart</h1>
      <div v-if="cartItemCount === 0">Your cart is empty.</div>
      <div v-else>
        <div v-for="item in cart" :key="item.bookId" class="cart-item">
          <img :src="item.imageUrl" alt="Product Image" class="cart-item-image" />
          <div class="cart-item-info">
            <h3>{{ item.title }}</h3>
            <p>{{ item.price }} ₹</p>
          </div>
        </div>
        <div class="cart-summary">
          <h2>Total: {{ cartTotal }} ₹</h2>
          <button @click="clearCart">Clear Cart</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { mapGetters, mapActions } from 'vuex';
  
  export default {
    name: 'CartView',
    computed: {
      ...mapGetters(['cart', 'cartItemCount', 'cartTotal'])
    },
    methods: {
      ...mapActions(['clearCart'])
    }
  };
  </script>
  
  <style scoped>
  .cart-view-container {
    padding: 40px;
    background-color: #f5f5f5;
  }
  
  .cart-item {
    display: flex;
    align-items: center;
    border-bottom: 1px solid #ddd;
    padding: 10px 0;
  }
  
  .cart-item-image {
    width: 100px;
    height: auto;
    border-radius: 8px;
    margin-right: 15px;
  }
  
  .cart-item-info {
    flex: 1;
  }
  
  .cart-item-info h3 {
    margin: 0;
    font-size: 1.25rem;
  }
  
  .cart-item-info p {
    margin: 5px 0;
    color: #007bff;
  }
  </style>
  