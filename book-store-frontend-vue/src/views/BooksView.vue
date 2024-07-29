<!-- <template>
  <div class="product-list-container">
    <div class="product-card" v-for="book in paginatedBooks" :key="book.bookId" @click="goToDetail(book.bookId)">
      <img :src="book.imageUrl" alt="Product Image" class="product-image" />
      <p class="product-name">{{ book.title }}</p>
      <p class="product-price"> {{ (book.price).toFixed(2) }} ₹</p>
      <button class="shop-now-btn">Shop now</button>
    </div>
    <div class="pagination">
      <button class="pagination-btn" @click="previousPage">Previous</button>
      <button class="pagination-btn" v-for="n in totalPages" :key="n" @click="setPage(n)">{{ n }}</button>
      <button class="pagination-btn" @click="nextPage">Next</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'BooksView',
  data() {
    return {
      books: [],
      currentPage: 1,
      booksPerPage: 12, // Number of books per page
      totalPages: 1
    };
  },
  computed: {
    paginatedBooks() {
      const start = (this.currentPage - 1) * this.booksPerPage;
      const end = start + this.booksPerPage;
      return this.books.slice(start, end);
    }
  },
  methods: {
    async fetchBooks() {
      try {
        const response = await axios.get('https://localhost:7044/api/Book');
        this.books = response.data;
        this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    },
    goToDetail(id) {
      this.$router.push({ name: 'ProductDetail', params: { id } });
    },
    previousPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    setPage(page) {
      this.currentPage = page;
    }
  },
  created() {
    this.fetchBooks();
  }
};
</script> -->

<template>
  <div class="product-list-container">
    <div class="product-card" v-for="book in paginatedBooks" :key="book.bookId" @click="goToDetail(book.bookId)">
      <img :src="book.imageUrl" alt="Product Image" class="product-image" />
      <p class="product-name">{{ book.title }}</p>
      <p class="product-price"> {{ (book.price).toFixed(2) }} ₹</p>
      <button class="shop-now-btn">Shop now</button>
    </div>
    <div class="pagination">
      <button class="pagination-btn" @click="previousPage">Previous</button>
      <button class="pagination-btn" v-for="n in totalPages" :key="n" @click="setPage(n)">{{ n }}</button>
      <button class="pagination-btn" @click="nextPage">Next</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'BooksView',
  data() {
    return {
      books: [],
      currentPage: 1,
      booksPerPage: 12,
      totalPages: 1
    };
  },
  computed: {
    paginatedBooks() {
      const start = (this.currentPage - 1) * this.booksPerPage;
      const end = start + this.booksPerPage;
      return this.books.slice(start, end);
    }
  },
  methods: {
    async fetchBooks() {
      try {
        const response = await axios.get('https://localhost:7044/api/Book');
        this.books = response.data;
        this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    },
    goToDetail(bookId) {
      this.$router.push({ name: 'ProductDetail', params: { id: bookId } });
    },
    previousPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    setPage(page) {
      this.currentPage = page;
    }
  },
  created() {
    this.fetchBooks();
  }
};
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
