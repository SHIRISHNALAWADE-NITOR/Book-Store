<template>
  <div class="product-list-container">
    <div class="product-card" v-for="book in paginatedBooks" :key="book.bookId" @click="goToDetail(book.bookId)">
      <img :src="book.imageUrl" alt="Product Image" class="product-image" />
      <p class="product-name">{{ book.title }}</p>
      <p class="product-price">{{ (book.price).toFixed(2) }} ₹</p>
    </div>
    <div class="pagination-controls">
      <button @click="previousPage" :disabled="currentPage === 1">Previous</button>
      <span>Page {{ currentPage }} of {{ totalPages }}</span>
      <button @click="nextPage" :disabled="currentPage === totalPages">Next</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { ref, computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';


export default {
  name: 'BooksView',
  setup() {
    const books = ref([]);
    const currentPage = ref(1);
    const booksPerPage = 15;
    const totalPages = ref(1);
    const route = useRoute();
    const router = useRouter();

    // Fetch books based on category from query
    const fetchBooks = async () => {
      try {
        const category = route.query.category;
        if (category) {
          const response = await axios.get(`http://localhost:5134/api/Book/category/${category}`);
          books.value = response.data;
        } else {
          // Fetch all books if no category is specified
          const response = await axios.get('http://localhost:5134/api/Book');
          books.value = response.data;
        }
        totalPages.value = Math.ceil(books.value.length / booksPerPage);
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    };

    // Fetch books when component mounts
    onMounted(fetchBooks);

    // Computed property for paginated books
    const paginatedBooks = computed(() => {
      const start = (currentPage.value - 1) * booksPerPage;
      const end = start + booksPerPage;
      return books.value.slice(start, end);
    });

    // Navigate to book detail view
    const goToDetail = (bookId) => {
      router.push({ name: 'ProductDetail', params: { id: bookId } });
    };

    // Change page
    const previousPage = () => {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      if (currentPage.value > 1) {
        currentPage.value--;
      }
    };

    const nextPage = () => {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      if (currentPage.value < totalPages.value) {
        currentPage.value++;
      }
    };

    return {
      books,
      currentPage,
      booksPerPage,
      totalPages,
      paginatedBooks,
      goToDetail,
      previousPage,
      nextPage
    };
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
  /* Reduced width to make cards smaller */
  background: #f9f9f9;
  margin-bottom: 10px;
  /* Reduced bottom margin */
  padding: 8px;
  /* Reduced padding */
  text-align: center;
  border: 1px solid #000;
  border-radius: 5px;
  cursor: pointer;
  transform: scale(0.85);
  /* Scale down by 15% */
  transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
  /* Smooth transition effect for transform and shadow */
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
}

.product-card:hover {
  transform: scale(1);
  /* Restore to original size on hover */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  /* Add shadow effect on hover */
}

.product-image {
  width: 100%;
  height: 80%;
  margin-bottom: 8px;
  /* Reduced margin bottom */
}

.product-name {
  margin: 4px 0;
  /* Reduced margin */
  font-size: 1.1rem;
  /* Smaller font size */
  overflow: hidden;
  /* Hide overflowing text */
  text-overflow: ellipsis;
  /* Add ellipsis for overflowed text */
  white-space: nowrap;
  /* Prevent text from wrapping to the next line */
  display: block;
  /* Ensure the text block takes up space */
  width: 100%;
  /* Ensure the product name takes up the full width of its container */
}

.product-price {
  margin: 4px 0;
  /* Reduced margin */
  font-size: 1.1rem;
  /* Smaller font size */
}

.pagination-controls {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 20px 0;
  /* Margin top for spacing */
}

.pagination-controls button {
  margin: 0 0.5rem;
  /* Adjusted margin */
  padding: 0.5rem 1rem;
  /* Adjusted padding */
  font-size: 0.9rem;
  /* Smaller font size */
  border: 1px solid #ddd;
  /* Border for visibility */
  border-radius: 5px;
  /* Rounded corners */
  background-color: #fff;
  /* Background color */
  cursor: pointer;
  /* Pointer cursor */
  transition: background-color 0.2s, color 0.2s;
  /* Transition for hover effects */
}

.pagination-controls button:hover {
  background-color: black;
  /* Hover background color */
  color: #fff;
  /* Hover text color */
}

.pagination-controls button:disabled {
  cursor: not-allowed;
  /* Disabled cursor */
  opacity: 0.5;
  /* Disabled opacity */
}

.pagination-controls span {
  margin: 0 1rem;
  /* Margin around the page number display */
  font-size: 0.9rem;
  /* Smaller font size for readability */
}
</style>