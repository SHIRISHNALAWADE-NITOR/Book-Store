<!-- <template>
  <div class="cart-container">
    <div class="cart-items">
      <h1>My Cart</h1>
      <ul v-if="cartItems.length">
        <li v-for="item in cartItems" :key="item.cartItemId">
          <div class="cart-item">
            <img :src="item.book.imageUrl" alt="Book Image" class="book-image" />
            <div class="book-details">
              <h3>{{ item.book.title }}</h3>
              <p><strong>Quantity :</strong> {{ item.quantity }}</p>
              <p><strong>Price :</strong> {{ item.book.price.toFixed(2) }}</p>
              <button @click="deleteItem(item.cartItemId)">Delete</button>
            </div>
          </div>
        </li>
      </ul>
      <p v-else>No items in the cart.</p>
    </div>
    <div class="cart-summary" v-if="cartItems.length">
      <h2>Cart Summary</h2>
      <p><strong>Total Items:</strong> {{ totalQuantity }}</p>
      <p><strong>Total Price:</strong> {{ totalPrice.toFixed(2) }}</p>
      <button @click="checkout" class="checkout-button">Checkout</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css'; // Import Toastify CSS

export default {
  name: 'CartView',
  data() {
    return {
      cartItems: [], // Holds the cart items with book details
      userId: localStorage.getItem('userid'), // Retrieve user ID from localStorage
    };
  },
  async mounted() {
    if (this.userId) {
      await this.fetchCartItems();
    } else {
      // Handle case where user is not logged in
      this.cartItems = [];
      this.$router.push('/login'); // Redirect to login page
    }
  },
  methods: {
    async fetchCartItems() {
      try {
        const response = await axios.get(`http://localhost:5134/api/CartItem/user/${this.userId}`);
        const cartItems = response.data;

        // Fetch book details for each cart item
        const detailedCartItems = await Promise.all(cartItems.map(async (item) => {
          const bookResponse = await axios.get(`http://localhost:5134/api/Book/${item.bookId}`);
          return { ...item, book: bookResponse.data };
        }));

        this.cartItems = detailedCartItems;
      } catch (error) {
        console.error('Error fetching cart items or book details:', error);
        // Optionally handle errors (e.g., redirect to login or show an error message)
      }
    },
    async deleteItem(cartItemId) {
      try {
        // Send request to delete the item
        await axios.delete(`http://localhost:5134/api/CartItem/${cartItemId}`);
        // Remove item from local state
        this.cartItems = this.cartItems.filter(item => item.cartItemId !== cartItemId);

        // Show success toast notification
        Toastify({
          text: "Item successfully deleted.",
          backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
          duration: 3000,
          close: true
        }).showToast();
      } catch (error) {
        console.error('Error deleting cart item:', error);

        // Show error toast notification
        Toastify({
          text: "Failed to delete item.",
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
      }
    },
    checkout() {
      // Implement checkout logic here
      // This might include validating cart items, redirecting to payment, etc.
    }
  },
  computed: {
    totalQuantity() {
      return this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
    },
    totalPrice() {
      return this.cartItems.reduce((sum, item) => sum + (item.book.price * item.quantity), 0);
    },
  },
};
</script> -->



<template>
  <div class="cart-container">
    <div class="cart-items">
      <h1>My Cart</h1>
      <ul v-if="cartItems.length">
        <li v-for="item in cartItems" :key="item.cartItemId">
          <div class="cart-item">
            <img :src="item.book.imageUrl" alt="Book Image" class="book-image" />
            <div class="book-details">
              <h3>{{ item.book.title }}</h3>
              <p><strong>Quantity :</strong> {{ item.quantity }}</p>
              <p><strong>Price :</strong> {{ item.book.price.toFixed(2) }}</p>
              <button @click="deleteItem(item.cartItemId)">Delete</button>
            </div>
          </div>
        </li>
      </ul>
      <p v-else>No items in the cart.</p>
    </div>
    <div class="cart-summary" v-if="cartItems.length">
      <h2>Cart Summary</h2>
      <p><strong>Total Items:</strong> {{ totalQuantity }}</p>
      <p><strong>Total Price:</strong> {{ totalPrice.toFixed(2) }}</p>
      <button @click="checkout" class="checkout-button">Checkout</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css'; // Import Toastify CSS

export default {
  name: 'CartView',
  data() {
    return {
      cartItems: [], // Holds the cart items with book details
      userId: localStorage.getItem('userid'), // Retrieve user ID from localStorage
    };
  },
  async mounted() {
    if (this.userId) {
      await this.fetchCartItems();
    } else {
      // Handle case where user is not logged in
      this.cartItems = [];
      this.$router.push('/login'); // Redirect to login page
    }
  },
  methods: {
    async fetchCartItems() {
      try {
        const response = await axios.get(`http://localhost:5134/api/CartItem/user/${this.userId}`);
        const cartItems = response.data;

        // Fetch book details for each cart item
        const detailedCartItems = await Promise.all(cartItems.map(async (item) => {
          const bookResponse = await axios.get(`http://localhost:5134/api/Book/${item.bookId}`);
          return { ...item, book: bookResponse.data };
        }));

        this.cartItems = detailedCartItems;
      } catch (error) {
        console.error('Error fetching cart items or book details:', error);
        // Optionally handle errors (e.g., redirect to login or show an error message)
      }
    },
    async deleteItem(cartItemId) {
      try {
        // Send request to delete the item
        await axios.delete(`http://localhost:5134/api/CartItem/${cartItemId}`);
        // Remove item from local state
        this.cartItems = this.cartItems.filter(item => item.cartItemId !== cartItemId);

        // Show success toast notification
        Toastify({
          text: "Item successfully deleted.",
          backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
          duration: 3000,
          close: true
        }).showToast();
      } catch (error) {
        console.error('Error deleting cart item:', error);

        // Show error toast notification
        Toastify({
          text: "Failed to delete item.",
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
      }
    },

    // async checkout() {
    //   try {
    //     // Check if user has an address
    //     const addressResponse = await axios.get(`http://localhost:5134/api/Address/${this.userId}`);
    //     if (addressResponse.data && addressResponse.data.length > 0) {
    //       // Address exists, proceed to OrderSummary component
    //       this.$router.push('/order-summary');
    //     } else {
    //       // No address, show a toast message and redirect to profile
    //       Toastify({
    //         text: "Please enter your address before checking out.",
    //         backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
    //         duration: 3000,
    //         close: true
    //       }).showToast();
    //       this.$router.push('/profile');
    //     }
    //   } catch (error) {
    //     console.error('Error checking address:', error);

    //     // Show error toast notification
    //     Toastify({
    //       text: "Error checking address.",
    //       backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
    //       duration: 3000,
    //       close: true
    //     }).showToast();
    //   }
    // }

    async checkout() {
      try {
        const addressResponse = await axios.get(`http://localhost:5134/api/Address/${this.userId}`);
        if (addressResponse.data && addressResponse.data.length > 0) {
          // Serialize cartItems into a query string
          const queryString = encodeURIComponent(JSON.stringify(this.cartItems));
          this.$router.push(`/order-summary?cartItems=${queryString}`);
        } else {
          Toastify({
            text: "Please enter your address before checking out.",
            backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
            duration: 3000,
            close: true
          }).showToast();
          this.$router.push('/profile');
        }
      } catch (error) {
        console.error('Error checking address:', error);
        Toastify({
          text: "Error checking address.",
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
      }
    }

  },
  computed: {
    totalQuantity() {
      return this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
    },
    totalPrice() {
      return this.cartItems.reduce((sum, item) => sum + (item.book.price * item.quantity), 0);
    },
  },
};
</script>


<style scoped>
.cart-container {
  display: flex;
  justify-content: space-between;
}

.cart-items {
  flex: 3;
}

.cart-summary {
  flex: 1;
  padding: 20px;
  border-left: 1px solid #ddd;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  margin-bottom: 10px;
}

.cart-item {
  display: flex;
  align-items: center;
  border: 1px solid #ddd;
  padding: 10px;
  border-radius: 5px;
}

.book-image {
  width: 100px;
  height: 150px;
  margin-right: 20px;
}

.book-details {
  flex: 1;
}

button {
  background-color: #d9534f;
  color: white;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 3px;
}

button:hover {
  background-color: #c9302c;
}

.checkout-button {
  background-color: #5bc0de;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 3px;
  font-size: 16px;
  margin-top: 20px;
}

.checkout-button:hover {
  background-color: #31b0d5;
}
</style>
