<template>
  <div class="product-detail-container" v-if="book">
    <div class="product-detail">
      <!-- Product Images and Info -->
      <div class="product-images">
        <img :src="book.imageUrl" alt="Main Image" class="main-image" />
        <div class="thumbnail-images">
          <img v-for="(thumbnail, index) in book.thumbnails" :key="index" :src="thumbnail" alt="Thumbnail"
            class="thumbnail-image" />
        </div>
      </div>
      <div class="product-info">
        <!-- Product Info -->
        <div class="info-header">
          <h1>{{ book.title }}</h1>
          <div class="rating">{{ book.rating }} ★</div>
        </div>
        <p class="author"><strong>Author</strong> {{ book.author }}</p>
        <p class="category"><strong>Category</strong> {{ book.category }}</p>
        <p class="pages"><strong>Number of Pages</strong> {{ book.numberOfPages }}</p>
        <p class="price">₹&nbsp;{{ book.price }} </p>
        <p class="description" @mouseover="showFullDescription" @mouseleave="hideFullDescription"
          :class="{ 'show-full': isDescriptionFull }">
          {{ book.description }}
        </p>
        <div class="quantity-container">
          <button @click="decrementQuantity">-</button>
          <input type="text" v-model.number="quantity" readonly />
          <button @click="incrementQuantity">+</button>
        </div>
        <button class="add-to-cart-btn" @click="handleAddToCart">Add To Cart</button>
      </div>
    </div>
    <h2>Similar Products</h2>
    <div class="similar-products">
      <div v-if="similarProducts.length > 0" class="similar-product-grid">
        <div v-for="product in similarProducts" :key="product.bookId" class="similar-product-card"
          @click="handleClick(product)">
          <img :src="product.imageUrl" alt="Similar Product Image" class="similar-product-image" />
          <div class="similar-product-name">{{ product.title }}</div>
          <div style="color: black;" class="similar-product-price">₹{{ product.price }} </div>
        </div>
      </div>
      <div v-else>No similar products found.</div>
    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex';
import axios from 'axios';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';

export default {
  name: 'IndividualProductView',
  props: {
    id: String
  },

  data() {
    return {
      book: null,
      quantity: 1,
      isDescriptionFull: false,
      similarProducts: [] // Data property for similar products
    };
  },

  async created() {
    await this.fetchBookDetails();
    await this.fetchSimilarProducts(); // Fetch similar products after book details
  },

  watch: {
    '$route.params.id': 'fetchData'
  },

  methods: {
    ...mapActions(['addToCart']),

    async fetchData() {
      await this.fetchBookDetails();
      await this.fetchSimilarProducts();
    },

    async fetchBookDetails() {
      try {
        const response = await axios.get(`http://localhost:5134/api/Book/${this.$route.params.id}`);
        this.book = response.data;
      } catch (error) {
        console.error('Error fetching book details:', error);
      }
    },

    async fetchSimilarProducts() {
      if (this.book && this.book.category) {
        try {
          const response = await axios.get(`http://localhost:5134/api/Book/category/${this.book.category}`);
          this.similarProducts = response.data.slice(0, 12); // Get top 12 products
        } catch (error) {
          console.error('Error fetching similar products:', error);
        }
      }
    },

    async handleAddToCart() {
      const userId = localStorage.getItem('userid');

      if (!userId) {
        Toastify({
          text: "Please log in to add items to the cart.",
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
        return;
      }

      if (this.book) {
        const payload = {
          cartItemId: 0,
          userId: userId,
          bookId: this.$route.params.id,
          quantity: this.quantity
        };
        try {
          await axios.post('http://localhost:5134/api/CartItem', payload);
          Toastify({
            text: "Added to cart!",
            backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
            duration: 3000,
            close: true
          }).showToast();
        } catch (error) {
          console.error('Error adding to cart:', error);
          Toastify({
            text: "Failed to add to cart.",
            backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
            duration: 3000,
            close: true
          }).showToast();
        }
      }
    },

    incrementQuantity() {
      this.quantity++;
    },

    decrementQuantity() {
      if (this.quantity > 1) {
        this.quantity--;
      }
    },

    showFullDescription() {
      this.isDescriptionFull = true;
    },

    hideFullDescription() {
      this.isDescriptionFull = false;
    },

    handleClick(product) {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      this.$router.push({ name: 'ProductDetail', params: { id: product.bookId } });
    }
  }
};
</script>


<style scoped>
.product-detail-container {
  padding: 40px;
  background-color: #f5f5f5;
}

.product-detail {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  margin-bottom: 30px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  padding: 20px;
}

.product-info {
  flex: 1;
  max-width: 600px;
  text-align: left;
}

.info-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 15px;
}

.info-header h1 {
  font-size: 2rem;
  font-weight: bold;
  margin: 0;
}

.rating {
  font-size: 1.5rem;
  color: #f39c12;
  /* Gold color for star rating */
}

.author,
.category,
.pages {
  font-size: 1.1rem;
  color: #333;
  margin-bottom: 10px;
}

.price {
  font-size: 1.5rem;
  font-weight: bold;
  color: #333;
  margin-bottom: 20px;
}

.description {
  font-size: 0.875rem;
  line-height: 1.6;
  margin-bottom: 30px;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  cursor: pointer;
  transition: all 0.3s ease;
}

.description.show-full {
  white-space: normal;
  overflow: visible;
}

.quantity-container {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.quantity-container button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px;
  cursor: pointer;
  border-radius: 5px;
  font-size: 1rem;
  margin: 0 5px;
}

.quantity-container input {
  width: 50px;
  text-align: center;
  border: 1px solid #ddd;
  border-radius: 5px;
  font-size: 1rem;
}

.add-to-cart-btn {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 12px 20px;
  cursor: pointer;
  border-radius: 5px;
  font-size: 1rem;
  transition: background-color 0.3s ease;
}

.add-to-cart-btn:hover {
  background-color: #0056b3;
}

.product-images {
  flex: 1;
  max-width: 350px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.main-image {
  width: 100%;
  max-width: 250px;
  height: auto;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.thumbnail-images {
  display: flex;
  gap: 10px;
  margin-top: 15px;
}

.thumbnail-image {
  width: 60px;
  height: auto;
  cursor: pointer;
  border: 2px solid transparent;
  border-radius: 4px;
  transition: border-color 0.3s ease;
}

.thumbnail-image:hover {
  border-color: #007bff;
}

.similar-products {
  margin-top: 30px;
}

.similar-products h2 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 20px;
}

.similar-product-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  /* Adjust gap between cards */
}

.similar-product-card {
  flex: 1 1 calc(25% - 10px);
  /* 25% width minus gap to fit 4 cards per row */
  background: white;
  cursor: pointer;
  padding: 0;
  /* Remove padding inside the card */
  text-align: center;
  border: 1px solid #ddd;
  border-radius: 6px;
  /* Border radius for the card */
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  /* Card shadow */
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  min-width: 120px;
  /* Ensure cards are not too small */
}

.similar-product-card:hover {
  transform: translateY(-2px);
  /* Hover effect */
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.15);
}

.similar-product-image {
  width: 100%;
  /* Ensure the image fills the card width */
  height: 300px;
  /* Increased height for book cover aspect */
  object-fit: contain;
  /* Ensure the entire image is visible */
  border-radius: 4px 4px 0 0;
  /* Rounded corners at the top */
  margin-bottom: 8px;
  /* Space between image and text */
}

.similar-product-name {
  font-size: 0.9rem;
  /* Adjusted font size */
  font-weight: bold;
  margin-bottom: 4px;
  /* Space below the name */
}

.similar-product-price {
  font-size: 0.9rem;
  /* Adjusted font size */
  color: #007bff;
  margin-bottom: 8px;
  /* Space below the price */
}

@media (max-width: 1024px) {
  .similar-product-card {
    flex: 1 1 calc(33.333% - 10px);
    /* Adjust for three items per row on medium screens */
  }
}

@media (max-width: 768px) {
  .similar-product-card {
    flex: 1 1 calc(50% - 10px);
    /* Adjust for two items per row on smaller screens */
  }
}

@media (max-width: 480px) {
  .similar-product-card {
    flex: 1 1 100%;
    /* Single item per row on very small screens */
  }
}

@media (max-width: 768px) {
  .product-detail {
    flex-direction: column;
  }

  .product-info {
    max-width: 100%;
  }

  .product-images {
    max-width: 100%;
    align-items: flex-start;
  }
}
</style>
