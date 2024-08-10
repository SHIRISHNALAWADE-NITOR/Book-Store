<template>
  <header class="header">
    <div class="container">
      <img src="../assets/PustakParadiselogo.png" class="brandlogo" alt="Pustak Paradise Logo">
      <div class="logo">
        <h1><strong>Pustak Paradise</strong> </h1>
      </div>
      <div v-if="isBooksPage" class="search-bar">
        <input type="text" placeholder="Search books..." v-model="searchQuery" @input="performSearch">
        <button @click="performSearch">Search</button>
        <div v-if="searchQuery && searchResults.length" class="search-suggestions">
          <ul>
            <li v-for="book in searchResults" :key="book.bookId" @click="viewBookDetails(book)">
              {{ book.title }}
            </li>
          </ul>
        </div>

        <div v-if="searchQuery && searchResults.length" class="search-suggestions">
          <ul>
            <li v-for="book in searchResults" :key="book.bookId" @click="viewBookDetails(book)">
              {{ book.title }} by {{ book.author }}
            </li>
          </ul>
        </div>
      </div>
      <nav class="nav">
        <ul>
          <li><a href="/"><i class="fa-solid fa-house"></i>&nbsp;Home</a></li>
          <li><a href="/books"><i class="fa-solid fa-book"></i>&nbsp;Books</a></li>
          <li><a href="/cart"><i class="fa-solid fa-cart-shopping"></i>&nbsp;Cart</a></li>
          <li v-if="isLoggedIn"><a href="/profile"><i class="fa-solid fa-user"></i>&nbsp;Profile</a></li>

          <li>
            <a v-if="!isLoggedIn" href="/login"><i class="fa-solid fa-right-from-bracket"></i>&nbsp;Login</a>
            <a v-else href="#" @click.prevent="logout"><i class="fa-solid fa-right-from-bracket"></i>&nbsp;Logout</a>
          </li>
        </ul>
      </nav>
    </div>
  </header>
</template>

<script>
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';
import axios from 'axios';

export default {
  name: 'HeaderComponent',
  data() {
    return {
      isLoggedIn: false,
      searchQuery: '',
      books: [],
      searchResults: [],
    };
  },
  computed: {
    isBooksPage() {
      return this.$route.path === '/books';
    },
    
  },
  created() {
    this.checkAuthStatus();
    this.fetchBooks();
    // Listen to route changes to update authentication status
    this.$router.afterEach(() => {
      this.checkAuthStatus();
    });
  },
  methods: {
    async fetchBooks() {
      try {
        const response = await axios.get('http://localhost:5134/api/Book');
        this.books = response.data;
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    },
    performSearch() {
      if (this.searchQuery.trim()) {
        this.searchResults = this.books
          .filter(book =>
            book.title.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
            book.author.toLowerCase().includes(this.searchQuery.toLowerCase())
          )
          .map(book => ({ bookId: book.bookId, title: book.title, author: book.author }))
          .slice(0, 10);
      } else {
        this.searchResults = [];
      }
    },
    viewBookDetails(book) {
      this.$router.push({ name: 'ProductDetail', params: { id: book.bookId } });
    },
    checkAuthStatus() {
      this.isLoggedIn = !!localStorage.getItem('authToken');
    },
    async logout() {
      await this.$store.dispatch('logout');
      localStorage.removeItem('authToken');
      localStorage.removeItem('userid');
      this.isLoggedIn = false; // Update state immediately
      Toastify({
        text: "Logout successful!",
        backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
        duration: 3000,
        close: true
      }).showToast();
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped>
.header {
  position: relative;

  padding: 10px 0;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  margin-top: -61px;
}

.container {
  display: flex;
  align-items: center;
  justify-content: space-between;
  max-width: 100%;
  margin: 0 auto;
  padding: 0 2%;
  position: relative;
}

.logo {
  display: flex;
  align-items: center;
}

.logo h1 {
  font-size: 24px;
  color: black;
  margin: 0;
  margin-left: 10px;
}

.brandlogo {
  width: 80px;
}

.search-bar {
  display: flex;
  align-items: center;
  position: relative;
  flex: 1;
  /* Allows the search bar to grow and occupy available space */
  justify-content: center;
  /* Centers the search bar */
}

.search-bar input {
  padding: 5px;
  font-size: 16px;
  width: 100%;
  /* Allows the input to stretch within the container */
  max-width: 300px;
  /* Limits the maximum width */
}

.search-bar button {
  padding: 5px 10px;
  font-size: 16px;
  cursor: pointer;
}

.search-suggestions {
  position: absolute;
  top: calc(100% + 5px);
  left: 0;
  background: white;
  border: 1px solid #ccc;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  width: 100%;
  z-index: 1000;
}

.search-suggestions ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.search-suggestions li {
  padding: 10px;
  cursor: pointer;
}

.search-suggestions li:hover {
  background-color: #f1f1f1;
}

.nav {
  display: flex;
  /* Ensures that navigation items are aligned */
  margin-left: auto;
}

.nav ul {
  list-style: none;
  display: flex;
  gap: 20px;
  margin: 0;
  padding: 0;
}

.nav a {
  text-decoration: none;
  color: black;
  font-weight: bold;
  transition: color 0.3s ease;
}

.nav a:hover {
  background-color: #CCD5AE;
}

/* Media query for responsiveness */
@media (max-width: 768px) {
  .container {
    padding: 0 5%;
  }

  .logo h1 {
    font-size: 20px;
  }

  .nav ul {
    flex-direction: column;
  }

  .nav li {
    margin-bottom: 10px;
  }

  .search-bar {
    margin: 10px 0;
    /* Adds margin for spacing on small screens */
  }
}
</style>
