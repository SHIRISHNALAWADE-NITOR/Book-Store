<template>
    <div class="order-history-container">
      <h1>Order History</h1>
      <div v-if="orders.length">
        <div v-for="order in orders" :key="order.orderId" class="order-card">
          <p><strong>Order ID:</strong> {{ order.orderId }}</p>
          <p><strong>Date:</strong> {{ formatDate(order.orderDate) }}</p>
          <p><strong>Total Amount:</strong> {{ formatCurrency(order.totalAmount) }}</p>
          <p><strong>Shipping Address ID:</strong> {{ order.shippingAddressId }}</p>
          <button @click="viewOrderDetails(order.orderId)">View Details</button>
        </div>
      </div>
      <p v-else>No orders found.</p>
    </div>
  </template>
  
  <script>
  export default {
    name: 'OrderHistory',
    data() {
      return {
        orders: []
      };
    },
    async created() {
      await this.fetchOrderData();
    },
    methods: {
      async fetchOrderData() {
        try {
          const userId = localStorage.getItem('userid');
          if (userId) {
            const response = await fetch(`http://localhost:5134/api/Order/user/${userId}`);
            if (response.ok) {
              this.orders = await response.json();
            } else {
              console.error('Failed to fetch orders');
            }
          }
        } catch (error) {
          console.error('Error fetching orders:', error);
        }
      },
      formatDate(dateString) {
        return new Date(dateString).toLocaleDateString();
      },
      formatCurrency(value) {
        if (!value) return '$0.00';
        return parseFloat(value).toLocaleString('en-US', { style: 'currency', currency: 'USD' });
      },
      viewOrderDetails(orderId) {
        this.$router.push({ name: 'OrderDetails', params: { orderId } });
      }
    }
  };
  </script>
  
  <style scoped>
  .order-history-container {
    padding: 20px;
    text-align: left;
  }
  
  .order-history-container h1 {
    font-size: 2rem;
    margin-bottom: 20px;
  }
  
  .order-card {
    border: 1px solid #d0d0d0;
    padding: 15px;
    margin-bottom: 15px;
    border-radius: 8px;
    background-color: #f9f9f9;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  .order-card p {
    margin: 5px 0;
  }
  
  .order-card button {
    background-color: #007bff;
    color: #ffffff;
    padding: 8px 16px;
    font-size: 0.875rem;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.3s, transform 0.2s;
  }
  
  .order-card button:hover {
    background-color: #0056b3;
    transform: scale(1.02);
  }
  </style>
  