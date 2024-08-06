<template>
    <div class="order-success">
      <h1>Order Successful!</h1>
      <p>Thank you for your purchase. Your order has been placed successfully.</p>
      <p><strong>Order Summary:</strong></p>
      <ul>
        <li v-for="item in orderItems" :key="item.bookId">
          <h3>{{ item.title }}</h3>
          <p><strong>Quantity:</strong> {{ item.quantity }}</p>
          <p><strong>Price:</strong> {{ item.price.toFixed(2) }}</p>
          <p><strong>Total Price for Item:</strong> {{ (item.price * item.quantity).toFixed(2) }}</p>
        </li>
      </ul>
      <p><strong>Total Quantity:</strong> {{ totalQuantity }}</p>
      <p><strong>Total Price:</strong> {{ totalPrice.toFixed(2) }}</p>
      <button @click="goHome">Go to Home</button>
    </div>
  </template>
  
  <script>
  export default {
    name: 'OrderSuccessView',
    data() {
      return {
        orderItems: [], // This will be populated from query params or state
        totalQuantity: 0,
        totalPrice: 0
      };
    },
    created() {
      // Fetch order data from query params or state
      const query = new URLSearchParams(this.$route.query);
      const orderItemsParam = query.get('orderItems');
      if (orderItemsParam) {
        try {
          this.orderItems = JSON.parse(decodeURIComponent(orderItemsParam));
          this.totalQuantity = this.orderItems.reduce((sum, item) => sum + item.quantity, 0);
          this.totalPrice = this.orderItems.reduce((sum, item) => sum + (item.price * item.quantity), 0);
        } catch (e) {
          console.error('Failed to parse order items:', e);
        }
      }
    },
    methods: {
      goHome() {
        this.$router.push('/');
      }
    }
  };
  </script>
  
  <style scoped>
  .order-success {
    padding: 20px;
    text-align: center;
  }
  
  h1 {
    color: #4CAF50;
  }
  
  button {
    padding: 10px 20px;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    margin-top: 20px;
  }
  </style>
  