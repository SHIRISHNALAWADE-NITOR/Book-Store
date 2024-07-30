<!-- <template>
  <div class="product-detail-container" v-if="book">
    <div class="product-detail">
      <div class="product-images">
        <img :src="book.imageUrl" alt="Main Image" class="main-image" />
        <div class="thumbnail-images">
          <img v-for="(thumbnail, index) in book.thumbnails" :key="index" :src="thumbnail" alt="Thumbnail"
            class="thumbnail-image" />
        </div>
      </div>
      <div class="product-info">
        <h2>{{ book.title }}</h2>
        <div class="reviews">
          <span>{{ book.rating }} ⭐</span>
          <span>{{ book.reviewsCount }} reviews</span>
        </div>
        <p class="price">{{ book.price }} ₹</p>
        <p class="description">{{ book.description }}</p>
        <button class="add-to-cart-btn">Add To Cart</button>
      </div>
    </div>
    <div class="similar-products">
      <h3>Similar Products</h3>
      <div class="similar-product-card" v-for="(similar, index) in book.similarProducts" :key="index">
        <img :src="similar.imageUrl" alt="Similar Product" class="similar-product-image" />
        <p class="similar-product-name">{{ similar.title }}</p>
        <p class="similar-product-price">{{ similar.price }} ₹</p>
        <div class="reviews">
          <span>{{ similar.rating }} ⭐</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'IndividualProductView',
  props: {
    id: String
  },
  data() {
    return {
      book: null
    };
  },
  async created() {
    await this.fetchBookDetails();
  },
  methods: {
    async fetchBookDetails() {
      try {
        const response = await axios.get(`https://localhost:7044/api/Book/${this.id}`);
        this.book = response.data;
      } catch (error) {
        console.error('Error fetching book details:', error);
      }
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

.product-images {
  flex: 1;
  max-width: 500px; /* Set a max width for the image section */
  display: flex;
  flex-direction: column;
  align-items: center;
}

.main-image {
  width: 100%;
  max-width: 400px; /* Adjusted max width for the main image */
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
  width: 80px;
  height: auto;
  cursor: pointer;
  border: 2px solid transparent;
  border-radius: 4px;
  transition: border-color 0.3s ease;
}

.thumbnail-image:hover {
  border-color: #007bff;
}

.product-info {
  flex: 1;
  max-width: 600px; /* Set a max width for the info section */
}

.product-info h2 {
  font-size: 1.75rem; /* Slightly smaller font size */
  font-weight: bold;
  margin-bottom: 15px;
}

.reviews {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
}

.reviews span {
  margin-right: 15px;
  font-size: 1rem;
}

.price {
  font-size: 1.5rem; /* Adjusted font size */
  font-weight: bold;
  color: #333;
  margin-bottom: 20px;
}

.description {
  font-size: 1rem;
  line-height: 1.6;
  margin-bottom: 30px;
}

.add-to-cart-btn {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 12px 20px; /* Slightly smaller padding */
  cursor: pointer;
  border-radius: 5px;
  font-size: 1rem;
  transition: background-color 0.3s ease;
}

.add-to-cart-btn:hover {
  background-color: #0056b3;
}

.similar-products {
  margin-top: 30px;
}

.similar-products h3 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 20px;
}

.similar-product-card {
  display: inline-block;
  width: calc(25% - 20px);
  background: white;
  margin: 10px;
  padding: 15px;
  text-align: center;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.similar-product-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}

.similar-product-image {
  width: 100%;
  height: auto;
  border-radius: 8px;
  margin-bottom: 15px;
}

.similar-product-name {
  font-size: 1rem;
  font-weight: bold;
  margin-bottom: 5px;
}

.similar-product-price {
  font-size: 1.2rem;
  color: #007bff;
  margin-bottom: 10px;
}

.reviews span {
  color: gold;
}

/* Responsive Styles */
@media (max-width: 768px) {
  .product-detail {
    flex-direction: column;
  }

  .product-info {
    max-width: 100%;
    padding-left: 0;
  }

  .product-images {
    max-width: 100%;
    align-items: flex-start; /* Align images to the start */
  }

  .similar-product-card {
    width: calc(50% - 20px);
  }
}

@media (max-width: 480px) {
  .similar-product-card {
    width: 100%;
  }
}

</style>  -->

<template>
  <div class="product-detail-container" v-if="book">
    <div class="product-detail">
      <div class="product-images">
        <img :src="book.imageUrl" alt="Main Image" class="main-image" />
        <div class="thumbnail-images">
          <img v-for="(thumbnail, index) in book.thumbnails" :key="index" :src="thumbnail" alt="Thumbnail"
            class="thumbnail-image" />
        </div>
      </div>
      <div class="product-info">
        <h1>{{ book.title }}</h1>
        <p class="author">Author: {{ book.author }}</p>
        <p class="category"><strong>Category:</strong> {{ book.category }}</p>
        <p class="pages"><strong>Number of Pages:</strong> {{ book.numberOfPages }}</p>
        <div class="reviews">
          <span>{{ book.rating }} ⭐</span>
          <span>{{ book.reviewsCount || 'No reviews' }} reviews</span>
        </div>
        <p class="price">{{ book.price }} ₹</p>
        <p class="description">{{ book.description }}</p>
        <button class="add-to-cart-btn" @click="handleAddToCart">Add To Cart</button>
      </div>
    </div>
    <div class="similar-products">
      <h2>Similar Products</h2>
      <div class="similar-product-card" v-for="(similar, index) in book.similarProducts" :key="index">
        <img :src="similar.imageUrl" alt="Similar Product" class="similar-product-image" />
        <p class="similar-product-name">{{ similar.title }}</p>
        <p class="similar-product-price">{{ similar.price }} ₹</p>
        <div class="reviews">
          <span>{{ similar.rating }} ⭐</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { mapActions } from 'vuex';

export default {
  name: 'IndividualProductView',
  props: {
    id: String
  },
  data() {
    return {
      book: null
    };
  },
  async created() {
    await this.fetchBookDetails();
  },
  methods: {
    ...mapActions(['addToCart']),
    
    async fetchBookDetails() {
      try {
        const response = await axios.get(`https://localhost:7044/api/Book/${this.id}`);
        this.book = response.data;
      } catch (error) {
        console.error('Error fetching book details:', error);
      }
    },
    
    handleAddToCart() {
      if (this.book) {
        this.addToCart(this.book);
      }
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

.product-images {
  flex: 1;
  max-width: 350px; /* Reduced max-width for the image section */
  display: flex;
  flex-direction: column;
  align-items: center;
}

.main-image {
  width: 100%;
  max-width: 250px; /* Further reduced max-width for the main image */
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
  width: 60px; /* Width for thumbnail images */
  height: auto;
  cursor: pointer;
  border: 2px solid transparent;
  border-radius: 4px;
  transition: border-color 0.3s ease;
}

.thumbnail-image:hover {
  border-color: #007bff;
}

.product-info {
  flex: 1;
  max-width: 600px; /* Max width for the info section */
}

.product-info h1 {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 15px;
}

.author {
  font-size: 1.1rem;
  color: #555;
  margin-bottom: 10px;
}

.category, .pages {
  font-size: 1.1rem;
  color: #333;
  margin-bottom: 10px;
}

.reviews {
  display: flex;
  align-items: center;
  margin-bottom: 15px;
}

.reviews span {
  margin-right: 15px;
  font-size: 1rem;
}

.price {
  font-size: 1.5rem;
  font-weight: bold;
  color: #333;
  margin-bottom: 20px;
}

.description {
  font-size: 0.875rem; /* Reduced font size for description */
  line-height: 1.6;
  margin-bottom: 30px;
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

.similar-products {
  margin-top: 30px;
}

.similar-products h2 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 20px;
}

.similar-product-card {
  display: inline-block;
  width: calc(25% - 20px);
  background: white;
  margin: 10px;
  padding: 15px;
  text-align: center;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.similar-product-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}

.similar-product-image {
  width: 100%;
  height: auto;
  border-radius: 8px;
  margin-bottom: 15px;
}

.similar-product-name {
  font-size: 1rem;
  font-weight: bold;
  margin-bottom: 5px;
}

.similar-product-price {
  font-size: 1.2rem;
  color: #007bff;
  margin-bottom: 10px;
}

.reviews span {
  color: gold;
}

/* Responsive Styles */
@media (max-width: 768px) {
  .product-detail {
    flex-direction: column;
  }

  .product-info {
    max-width: 100%;
    padding-left: 0;
  }

  .product-images {
    max-width: 100%;
    align-items: flex-start;
  }

  .similar-product-card {
    width: calc(50% - 20px);
  }
}

@media (max-width: 480px) {
  .similar-product-card {
    width: 100%;
  }
}
</style>





