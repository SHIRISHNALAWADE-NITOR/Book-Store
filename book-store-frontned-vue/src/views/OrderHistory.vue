<template>
  <div class="order-history-container">
    <h1>Order History</h1>
    <div v-if="orders.length">
      <div v-for="order in orders" :key="order.orderId" class="order-card">
        <p><strong>Order ID:</strong> {{ order.orderId }}</p>
        <p><strong>Date:</strong> {{ formatDate(order.orderDate) }}</p>
        <p><strong>Total Amount:</strong> ₹ {{ (order.totalAmount) }}</p>
        <p><strong>Shipping Address ID:</strong> {{ order.shippingAddressId }}</p>
        <button @click="viewOrderDetails(order)">View Details</button>
 
        <div v-if="selectedOrder && selectedOrder.orderId === order.orderId" class="order-items">
          <h3>Order Items</h3>
          <div v-for="item in selectedOrder.orderItems" :key="item.orderItemId" class="order-item">
            <div class="book-details-container">
              <img v-if="bookDetails[item.bookId]" :src="bookDetails[item.bookId].imageUrl" alt="Book Cover" class="book-image" />
              <div class="book-details">
                <p><strong>Book ID:</strong> {{ item.bookId }}</p>
                <p><strong>Quantity:</strong> {{ item.quantity }}</p>
                <p><strong>Price:</strong> ₹ {{ (item.price) }} </p>
                <p><strong>Title:</strong> {{ bookDetails[item.bookId].title }}</p>
                <p><strong>Author:</strong> {{ bookDetails[item.bookId].author }}</p>
                <div class="download-buttons">
                  <a :href="getDownloadUrl(item.bookId, 'audio')" class="download-button" download>Download Audio</a>
                  <a :href="getDownloadUrl(item.bookId, 'video')" class="download-button" download>Download Video</a>
                  <a :href="getDownloadUrl(item.bookId, 'pdf')" class="download-button" download>Download PDF</a>
                </div>
              </div>
            </div>
          </div>
        </div>
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
      orders: [],
      selectedOrder: null,
      bookDetails: {} // Store book details here
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
            const orders = await response.json();
            this.orders = orders;
 
            // Collect all unique book IDs
            const bookIds = new Set();
            for (const order of orders) {
              for (const item of order.orderItems) {
                bookIds.add(item.bookId);
              }
            }
 
            // Fetch all book details in parallel
            await Promise.all(Array.from(bookIds).map(bookId => this.fetchBookDetails(bookId)));
          } else {
            console.error('Failed to fetch orders');
          }
        }
      } catch (error) {
        console.error('Error fetching orders:', error);
      }
    },
    async fetchBookDetails(bookId) {
      try {
        const response = await fetch(`http://localhost:5134/api/Book/${bookId}`);
        if (response.ok) {
          const book = await response.json();
          console.log('Fetched book details:', book); // Debugging line
          this.bookDetails[bookId] = book; // Directly assign the value
        } else {
          console.error('Failed to fetch book details');
        }
      } catch (error) {
        console.error('Error fetching book details:', error);
      }
    },
    formatDate(dateString) {
      return new Date(dateString).toLocaleDateString();
    },
    viewOrderDetails(order) {
      this.selectedOrder = this.selectedOrder && this.selectedOrder.orderId === order.orderId ? null : order;
    },
    getDownloadUrl(bookId, fileType) {
      return `http://localhost:5134/api/files/${fileType}/${bookId}`;
    }
  }
};
</script>
 
<style scoped>
.order-history-container {
  padding: 20px;
  max-width: 1200px;
  margin: 0 auto;
  background-color: #f4f7f9;
  text-align: left;
}
 
h1 {
  font-size: 2.5rem;
  color: #333;
  margin-bottom: 20px;
  text-align: center;
}
 
.order-card {
  border: 1px solid #e0e0e0;
  padding: 20px;
  margin-bottom: 20px;
  border-radius: 10px;
  background-color: #ffffff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.3s ease, transform 0.3s ease;
}
 
.order-card:hover {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
  transform: translateY(-2px);
}
 
.order-card p {
  margin: 10px 0;
  color: #555;
}
 
.order-card button {
  background-color: #007bff;
  color: #ffffff;
  padding: 10px 20px;
  font-size: 1rem;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
}
 
.order-card button:hover {
  background-color: #0056b3;
  transform: scale(1.05);
}
 
.order-items {
  margin-top: 15px;
  border-top: 1px solid #e0e0e0;
  padding-top: 15px;
}
 
.order-item {
  margin-bottom: 15px;
  padding-bottom: 10px;
}
 
.book-details-container {
  display: flex;
  align-items: flex-start;
}
 
.book-image {
  max-width: 150px;
  height: auto;
  border-radius: 8px;
  margin-right: 20px;
}
 
.book-details {
  flex: 1;
  display: flex;
  flex-direction: column;
}
 
.book-details p {
  margin: 5px 0;
  color: #333;
}
 
.book-details img {
  display: block;
  margin-top: 10px;
}
 
.download-buttons {
  margin-top: 10px;
}
 
.download-button {
  display: inline-block;
  margin-right: 10px;
  padding: 8px 12px;
  color: #ffffff;
  background-color: #28a745;
  border-radius: 5px;
  text-decoration: none;
  font-size: 0.9rem;
  transition: background-color 0.3s ease, transform 0.2s ease;
}
 
.download-button:hover {
  background-color: #218838;
  transform: scale(1.05);
}
 
.no-orders {
  text-align: center;
  font-size: 1.25rem;
  color: #666;
}
</style>