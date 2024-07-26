<template>
    <div class="product-list-container">
      <div class="product-card" v-for="n in 12" :key="n" @click="goToDetail(n)">
        <img src="https://via.placeholder.com/150" alt="Product Image" class="product-image" />
        <p class="product-name">Product Name</p>
        <p class="product-price">$12</p>
        <p class="product-shipping">Free shipping</p>
        <button class="shop-now-btn">Shop now</button>
      </div>
      <div class="pagination">
        <button class="pagination-btn">Previous</button>
        <button class="pagination-btn" v-for="n in 16" :key="n">{{ n }}</button>
        <button class="pagination-btn">Next</button>
      </div>
      <footer class="footer">FOOTER</footer>
    </div>
  </template>
  
  <script>
  export default {
    name: 'BooksView',
    methods: {
      goToDetail(id) {
        this.$router.push({ name: 'ProductDetail', params: { id } });
      }
    }
  }
  </script>
  
  <style scoped>
  .product-list-container {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
    padding: 20px;
  }
  .product-card {
    width: 18%;
    background: #f9f9f9;
    margin-bottom: 20px;
    padding: 15px;
    text-align: center;
    border: 1px solid #ddd;
    border-radius: 5px;
    cursor: pointer;
  }
  .product-image {
    width: 100%;
    height: auto;
    margin-bottom: 10px;
  }
  .product-name,
  .product-price,
  .product-shipping {
    margin: 5px 0;
  }
  .shop-now-btn {
    background-color: #007bff;
    color: white;
    border: none;
    padding: 10px;
    cursor: pointer;
    border-radius: 5px;
  }
  .pagination {
    display: flex;
    justify-content: center;
    margin: 20px 0;
  }
  .pagination-btn {
    background: none;
    border: 1px solid #ddd;
    padding: 5px 10px;
    margin: 0 5px;
    cursor: pointer;
  }
  .footer {
    width: 100%;
    text-align: center;
    padding: 20px;
    background: #333;
    color: white;
  }
  </style>
  