<template>
  <div>
    <h1>Books and Reviews</h1>
    <div v-if="books.length">
      <div v-for="book in books" :key="book.id" class="book">
        <h2>{{ book.title }}</h2>
        <p>{{ book.author }}</p>
        <textarea v-model="newReview[book.id]" placeholder="Add a review"></textarea>
        <button @click="submitReview(book.id)">Submit Review</button>
      </div>
    </div>
    <div v-else>
      <p>Loading books...</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ReviewView',
  data() {
    return {
      books: [],
      newReview: {}
    };
  },
  methods: {
    async fetchBooks() {
      try {
        const response = await axios.get(`https://localhost:7044/api/Book/category/${id}`);
        this.books = response.data;
        // Initialize newReview object with empty strings
        this.books.forEach(book => {
          this.$set(this.newReview, book.id, '');
        });
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    },
    async submitReview(bookId) {
      try {
        const review = this.newReview[bookId];
        if (!review) {
          alert('Please enter a review before submitting.');
          return;
        }

        await axios.post(`https://your-backend-api.com/books/${bookId}/reviews`, { review });
        alert('Review submitted successfully!');
        // Optionally, clear the review input after submission
        this.$set(this.newReview, bookId, '');
      } catch (error) {
        console.error('Error submitting review:', error);
        alert('Failed to submit review. Please try again.');
      }
    }
  },
  created() {
    this.fetchBooks();
  }
};
</script>

<style scoped>
.book {
  border: 1px solid #ddd;
  padding: 16px;
  margin-bottom: 16px;
}
textarea {
  width: 100%;
  height: 60px;
  margin-top: 8px;
}
button {
  margin-top: 8px;
}
</style>
