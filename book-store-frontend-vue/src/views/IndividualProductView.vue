<!-- <template>
    <div class="product-detail-container">
      <div class="product-detail">
        <div class="product-images">
          <img src="https://via.placeholder.com/300" alt="Main Image" class="main-image" />
          <div class="thumbnail-images">
            <img src="https://via.placeholder.com/50" alt="Thumbnail" v-for="n in 3" :key="n" class="thumbnail-image" />
          </div>
        </div>
        <div class="product-info">
          <h2>Product Title</h2>
          <div class="reviews">
            <span>⭐⭐⭐⭐⭐</span>
            <span>5 reviews</span>
          </div>
          <p class="price">$1232</p>
          <select class="model-select">
            <option>Select Model</option>
            <option>Model 1</option>
            <option>Model 2</option>
          </select>
          <p class="description">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
          </p>
          <button class="add-to-cart-btn">Add To Cart</button>
        </div>
      </div>
      <div class="similar-products">
        <h3>Similar Products</h3>
        <div class="similar-product-card" v-for="n in 4" :key="n">
          <img src="https://via.placeholder.com/100" alt="Similar Product" class="similar-product-image" />
          <p class="similar-product-name">Product Name</p>
          <p class="similar-product-price">$300</p>
          <div class="reviews">
            <span>⭐⭐⭐⭐</span>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: 'IndividualProductView',
    props: ['id']
  }
  </script>
  
  <style scoped>
  .product-detail-container {
    padding: 20px;
  }
  .product-detail {
    display: flex;
    justify-content: space-between;
    margin-bottom: 20px;
  }
  .product-images {
    flex: 1;
  }
  .main-image {
    width: 100%;
    height: auto;
  }
  .thumbnail-images {
    display: flex;
    justify-content: space-around;
    margin-top: 10px;
  }
  .thumbnail-image {
    width: 50px;
    height: auto;
    cursor: pointer;
  }
  .product-info {
    flex: 1;
    padding-left: 20px;
  }
  .reviews span {
    margin-right: 10px;
  }
  .price {
    font-size: 1.5rem;
    color: #333;
  }
  .model-select {
    width: 100%;
    margin: 10px 0;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 5px;
  }
  .description {
    margin: 20px 0;
  }
  .add-to-cart-btn {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 10px 20px;
    cursor: pointer;
    border-radius: 5px;
  }
  .similar-products {
    margin-top: 20px;
  }
  .similar-product-card {
    display: inline-block;
    width: 23%;
    background: #f9f9f9;
    margin: 10px;
    padding: 15px;
    text-align: center;
    border: 1px solid #ddd;
    border-radius: 5px;
  }
  .similar-product-image {
    width: 100%;
    height: auto;
    margin-bottom: 10px;
  }
  .similar-product-name,
  .similar-product-price {
    margin: 5px 0;
  }
  .reviews span {
    color: gold;
  }
  </style>
   -->

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
  padding: 20px;
}

.product-detail {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
}

.product-images {
  flex: 1;
}

.main-image {
  width: 80%; /* Adjust as needed */
  max-width: 400px; /* Ensures the image doesn't grow too large */
  height: auto;
}

.thumbnail-images {
  display: flex;
  justify-content: space-around;
  margin-top: 10px;
}

.thumbnail-image {
  width: 40px; /* Adjusted size for smaller thumbnails */
  height: auto;
  cursor: pointer;
}

.product-info {
  flex: 1;
  padding-left: 20px;
}

.reviews span {
  margin-right: 10px;
}

.price {
  font-size: 1.5rem;
  color: #333;
}

.model-select {
  width: 100%;
  margin: 10px 0;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.description {
  margin: 20px 0;
}

.add-to-cart-btn {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
}

.similar-products {
  margin-top: 20px;
}

.similar-product-card {
  display: inline-block;
  width: 23%;
  background: #f9f9f9;
  margin: 10px;
  padding: 15px;
  text-align: center;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.similar-product-image {
  width: 100%;
  height: auto;
  transform: scaleY(0.7); /* Scales down the height by 30% */
  transform-origin: top; /* Ensures scaling happens from the top */
  margin-bottom: 10px;
}

.similar-product-name,
.similar-product-price {
  margin: 5px 0;
}

.reviews span {
  color: gold;
}

</style>