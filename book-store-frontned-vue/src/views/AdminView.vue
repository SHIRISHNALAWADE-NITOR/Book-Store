<template>
  <div class="admin-view">
    <header class="header">
      <h1>Admin Dashboard</h1>
       <!-- Search Input -->
      <div class="search-container">
        <input v-model="searchQuery" placeholder="Search books..." />
      </div>
    </header>
    <main class="main-content">
      <button @click="showAddBookModal" class="add-book-button">Add New Book</button>
      <!-- Modal -->
      <div v-if="showModal" class="modal-overlay" @click.self="hideModal">
        <div class="form-container modal-content">
          <h2>{{ editMode ? 'Edit Book' : 'Add Book' }}</h2>
          <!-- <form @submit.prevent="editMode ? updateBook() : addBook()">
            <label>
              Title
              <input v-model="currentBook.title" required />
            </label>
            <label>
              Author
              <input v-model="currentBook.author" required />
            </label>
            <label>
              Category
              <input v-model="currentBook.category" required />
            </label>
            <label>
              ISBN:
              <input v-model="currentBook.isbn" required />
            </label>
            <label>
              Number of Pages:
              <input type="number" v-model.number="currentBook.numberOfPages" required />
            </label>
            <label>
              Rating:
              <input type="number" v-model.number="currentBook.rating" step="0.1" min="0" max="5" required />
            </label>
            <label>
              Price:
              <input type="number" v-model.number="currentBook.price" step="0.01" min="0" required />
            </label>
            <label>
              Description:
              <textarea v-model="currentBook.description" required></textarea>
            </label>
            <label>
              Image URL:
              <input v-model="currentBook.imageUrl" required />
            </label>
            <div class="modal-buttons">
              <button type="submit">{{ editMode ? 'Update Book' : 'Add Book' }}</button>
              <button type="button" @click="hideModal">Cancel</button>
            </div>
          </form> -->
         
        <form @submit.prevent="editMode ? updateBook() : addBook()">
        <div class="container">
          <div class="row">
            <div class="col">
              <label for="isbn" class="form-label">ISBN</label>
              <input id="isbn"  v-model.number="currentBook.isbn"  name="isbn" class="form-control" type="number" required>
            </div>
            <div class="dropdown-container">
    <label for="category-select">Select a Category:</label>
    <select id="category-select" v-model="currentBook.category" name="category" required>
      <option value="" disabled>Select a category</option>
      <option v-for="category in categories" :key="category" :value="category">
        {{ category }}
      </option>
    </select>
  </div>

          </div>
          <div class="row">

            <div class="col">
              <label for="title" class="form-label">Title</label>
              <input id="title" v-model="currentBook.title" name="title" class="form-control" type="text" required>
             
            </div>
            <div class="col">
              <label for="author" class="form-label">Author</label>
              <input id="author" v-model="currentBook.author" name="author" class="form-control" type="text"
                required>
            </div>
          </div>
          <div class="row">
            <div class="col">
              <label for="price" class="form-label">Price</label>
              <input id="price" v-model="currentBook.price" name="price" class="form-control" type="number"
                step="0.01" required>
            </div>
            <div class="col"> <label for="numberOfPages" class="form-label">Number of Pages</label>
              <input id="numberOfPages"  v-model="currentBook.numberOfPages" name="numberOfPages"
                class="form-control" type="number">
            </div>
            <div class="col"> <label for="rating" class="form-label">Rating</label>
              <input id="rating"  v-model="currentBook.rating" name="rating" class="form-control" type="number"
                step="0.1">
            </div>
          </div>


          <div class="mb-3">
            <label for="description" class="form-label">Description</label>
            <textarea id="description"  v-model="currentBook.description" name="description"
              class="form-control"></textarea>
          </div>
          <div class="row">
            <div class="col">
              <label for="imageUrl" class="form-label">Image URL</label>
              <input id="imageUrl"  v-model="currentBook.imageUrl" name="imageUrl" class="form-control" type="text">
            </div>

            <div class="col">
              <label for="createdAt" class="form-label">Created At</label>
              <input id="createdAt"  v-model="currentBook.createdAt" name="createdAt" class="form-control"
                type="datetime-local">
            </div>
            <div class="col">
              <label for="quantity" class="form-label">Quantity </label>
              <input id="quantity"  v-model="currentBook.quantity" name="quantity" class="form-control"
                type="number">
            </div>


          </div>

          <br>
          <div class="d-flex justify-content-between">
            <button type="submit" class="btn btn-primary">{{ editMode ? 'Update Book' : 'Add Book' }}</button>
            <button type="button" class="btn btn-secondary"  @click="hideModal">Cancel</button>
          </div>
          <!-- <div class="modal-buttons">
              <button type="submit">{{ editMode ? 'Update Book' : 'Add Book' }}</button>
              <button type="button" @click="hideModal">Cancel</button>
            </div> -->

        </div>
      </form>
        </div>
      </div>

      <div class="book-list">
        <div v-for="book in paginatedBooks" :key="book.bookId" class="book-item">
          <img :src="book.imageUrl" alt="Book Cover" class="book-cover" />
          <div class="book-details">
            <h2>{{ book.title }}</h2>
            <p><strong>Author</strong> {{ book.author }}</p>
            <p><strong>Category</strong> {{ book.category }}</p>
            <div class="book-actions">
            <button @click="editBook(book)"><i class="fa-solid fa-pen-to-square"></i>&nbsp;<span style="font-size: medium;">Edit</span></button>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <button class="delete-icon" @click="confirmDelete(book.bookId)">
              <i class="fa-solid fa-trash"></i>&nbsp;<span style="font-size: medium;">Delete</span>
            </button>
          </div>
          </div>
          
        </div>
      </div>
      <div class="pagination-controls">
        <button @click="previousPage" :disabled="currentPage === 1">Previous</button>
        <span>Page {{ currentPage }} of {{ totalPages }}</span>
        <button @click="nextPage" :disabled="currentPage === totalPages">Next</button>
      </div>
    </main>
  </div>
</template>

<script>
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';

export default {
  name: 'AdminView',
  data() {
    return {
      books: [],
      currentPage: 1,
      booksPerPage: 15,
      totalPages: 1,
      showModal: false,
      editMode: false,
      currentBook: {
        bookId: 0,
        title: '',
        author: '',
        category: '',
        isbn: '',
        numberOfPages: 0,
        rating: 0,
        price: 0,
        description: '',
        imageUrl: '',
        quantity:0,
        createdAt:''
      },
      searchQuery: '',
      categories: ['Fiction,Biography & Autobiography','History','Science','Literary Criticism','Poetry','Art','Drama','Philosophy','Performing Arts','Social Science','Literary Collections','Juvenile Fiction','Juvenile Nonfiction','Language Arts & Disciplines','Adventure stories','Business & Economics','Psychology','Religion']
      
    };
  },
  computed: {
    filteredBooks() {
      const query = this.searchQuery.toLowerCase();
      return this.books.filter(book => {
        const title = (book.title || '').toLowerCase();
        const author = (book.author || '').toLowerCase();
        const category = (book.category || '').toLowerCase();
        const isbn = String(book.isbn || '').toLowerCase();  // Convert to string and then to lower case

        return title.includes(query) ||
          author.includes(query) ||
          category.includes(query) ||
          isbn.includes(query);
      });
    },
    paginatedBooks() {
      const start = (this.currentPage - 1) * this.booksPerPage;
      const end = start + this.booksPerPage;
      return this.filteredBooks.slice(start, end);
    }
  },
  methods: {
    fetchBooks() {
      fetch('http://localhost:5134/api/Book')
        .then(response => response.json())
        .then(data => {
          this.books = data;
          this.totalPages = Math.ceil(this.filteredBooks.length / this.booksPerPage); // Updated to use filteredBooks
        })
        .catch(error => console.error('Error fetching books:', error));
    },
    confirmDelete(bookId) {
      const confirmed = window.confirm('Are you sure you want to delete this book?');
      if (confirmed) {
        this.deleteBook(bookId);
      }
    },
    previousPage() {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    showAddBookModal() {
      this.resetForm();
      this.showModal = true;
    },
    hideModal() {
      this.showModal = false;
      this.resetForm();
    },
    addBook() {
      const token = localStorage.getItem('authToken');
      const headers = {
        'Content-Type': 'application/json'
      };
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }

      fetch('http://localhost:5134/api/Book', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(this.currentBook)
      })
        .then(response => response.json())
        .then(data => {
          this.books.push(data);
          this.totalPages = Math.ceil(this.filteredBooks.length / this.booksPerPage); // Updated to use filteredBooks
          this.hideModal();
          Toastify({
            text: "Book added successfully!",
            duration: 3000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "#4CAF50",
          }).showToast();
        })
        .catch(error => {
          console.error('Error adding book:', error);
          Toastify({
            text: "Failed to add book!",
            duration: 3000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "#dc3545",
          }).showToast();
        });
    },
    editBook(book) {
      window.scrollTo({
        top: 0,
        behavior: 'smooth'
      });
      this.currentBook = { ...book };
      this.editMode = true;
      this.showModal = true;
    },
    updateBook() {
      const token = localStorage.getItem('authToken');
      const headers = {
        'Content-Type': 'application/json'
      };
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }

      fetch(`http://localhost:5134/api/Book/${this.currentBook.bookId}`, {
        method: 'PUT',
        headers: headers,
        body: JSON.stringify(this.currentBook)
      })
        .then(response => {
          if (!response.ok) {
            return response.json().then(errorData => {
              throw new Error(`Error ${response.status}: ${errorData.message}`);
            });
          }
          return response.json();
        })
        .then(data => {
          const index = this.books.findIndex(book => book.bookId === this.currentBook.bookId);
          if (index !== -1) {
            this.books[index] = data;
          }
          this.hideModal();
          Toastify({
            text: "Book updated successfully!",
            duration: 3000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "#4CAF50",
          }).showToast();
        })
        .catch(error => {
          console.error('Error updating book:', error);
          Toastify({
            text: `Failed to update book! Error: ${error.message}`,
            duration: 3000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "#dc3545",
          }).showToast();
        });
    },
    deleteBook(bookId) {
      const token = localStorage.getItem('authToken');
      const headers = {
        'Content-Type': 'application/json'
      };
      if (token) {
        headers['Authorization'] = `Bearer ${token}`;
      }

      fetch(`http://localhost:5134/api/Book/${bookId}`, {
        method: 'DELETE',
        headers: headers
      })
        .then(response => {
          if (response.ok) {
            this.books = this.books.filter(book => book.bookId !== bookId);
            this.totalPages = Math.ceil(this.filteredBooks.length / this.booksPerPage); // Updated to use filteredBooks
            Toastify({
              text: "Book deleted successfully!",
              duration: 3000,
              close: true,
              gravity: "top",
              position: "right",
              backgroundColor: "#4CAF50",
            }).showToast();
          } else {
            response.json().then(errorData => {
              Toastify({
                text: `Failed to delete book! Error: ${errorData.message}`,
                duration: 3000,
                close: true,
                gravity: "top",
                position: "right",
                backgroundColor: "#dc3545",
              }).showToast();
            });
          }
        })
        .catch(error => {
          console.error('Error deleting book:', error);
          Toastify({
            text: "Failed to delete book!",
            duration: 3000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "#dc3545",
          }).showToast();
        });
    },
    resetForm() {
      this.currentBook = {
        bookId: 0,
        title: '',
        author: '',
        category: '',
        isbn: '',
        numberOfPages: 0,
        rating: 0,
        price: 0,
        description: '',
        imageUrl: ''
      };
      this.editMode = false;
    }
  },
  mounted() {
    this.fetchBooks();
  }
};
</script>

<style scoped>
.admin-view {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  font-family: Arial, sans-serif;
}
 
.header {
  background-color: #343a40;
  color: #fff;
  padding: 1rem 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
 
.header h1 {
  margin: 0;
  font-size: 1.5rem;
}
 
.main-content {
  flex: 1;
  padding: 2rem;
  background-color: #f8f9fa;
}
 
.add-book-button {
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
  background-color: #007bff;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  margin-bottom: 1.5rem;
  transition: background-color 0.3s;
}
 
.add-book-button:hover {
  background-color: #0056b3;
}
 
.add-book-form {
  margin-bottom: 2rem;
  background-color: #fff;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}
 
.add-book-form form {
  display: grid;
  gap: 1rem;
}
 
.add-book-form label {
  display: flex;
  flex-direction: column;
}
 
.add-book-form input {
  padding: 0.75rem;
  font-size: 1rem;
  border: 1px solid #ced4da;
  border-radius: 4px;
}
 
.add-book-form button {
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
  background-color: #28a745;
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}
 
.add-book-form button.cancel {
  background-color: #dc3545;
}
 
.add-book-form button.cancel:hover {
  background-color: #c82333;
}
 
.add-book-form button:hover {
  background-color: #218838;
}
 
.book-list {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  /* Display 3 books per row */
  gap: 1.5rem;
}
 
.book-item {
  display: flex;
  border: 1px solid #dee2e6;
  padding: 1rem;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}
 
.book-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}
 
.book-cover {
  max-width: 120px;
  max-height: 180px;
  margin-right: 1rem;
  object-fit: cover;
}
 
.book-details {
  text-align: left;
  /* white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 200px; */

}

 
.book-details h2 {
  margin: 0;
  font-size: 1.25rem;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 200px;
}
 
.book-details p {
  margin: 0.5rem 0;
  color: #495057;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  max-width: 200px;
}
 
.book-actions {
  display: block;
  gap: 0.5rem;
  margin-top: 1rem;
}
 
.book-actions button {
  padding: 0.25rem 0.5rem;
  /* Further reduced padding */
  font-size: 0.75rem;
  /* Smaller font size */
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s, opacity 0.3s;
}
 
.book-actions button:nth-child(1) {
  background-color: #007bff;
  color: #fff;
}
 
.book-actions button:nth-child(2) {
  background-color: #dc3545;
  color: #fff;
}
 
.book-actions button:hover {
  opacity: 0.8;
}
 
.book-actions button:nth-child(1):hover {
  background-color: #0056b3;
}
 
.book-actions button:nth-child(2):hover {
  background-color: #c82333;
}
 
.pagination-controls {
  display: flex;
  justify-content: center;
  gap: 1rem;
  margin-top: 2rem;
}
 
.pagination-controls button {
  padding: 0.5rem 1rem;
  font-size: 0.875rem;
  border: 1px solid #ced4da;
  border-radius: 4px;
  cursor: pointer;
  background-color: #fff;
  transition: background-color 0.3s, opacity 0.3s;
}
 
.pagination-controls button:hover {
  background-color: black;
  color: white;
 
}
 
.pagination-controls button:disabled {
  cursor: not-allowed;
  background-color: #f8f9fa;
  color: #6c757d;
}
 
.pagination-controls span {
  align-self: center;
  font-size: 1rem;
  color: #495057;
}
/* Modal overlay */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4); /* Slightly lighter overlay */
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  backdrop-filter: blur(5px); /* Adjust the blur amount as needed */
}
 
/* Modal content */
.modal-content {
  background: #fff;
  padding: 15px;
  border-radius: 8px;
  width: 100%;
  max-width: 500px; /* Smaller width */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  position: relative;
  z-index: 1001; /* Ensure it's above the overlay */
}
 
/* Modal header */
.modal-content h2 {
  margin: 0 0 15px;
  font-size: 1.4em;
  color: #333;
  font-weight: 600;
}
 
/* Form styling */
form {
  display: flex;
  flex-direction: column;
}
 
label {
  margin-bottom: 5px;
  font-weight: 500;
  color: #555;
}
 
/* Input and textarea styling */
input[type="text"],
input[type="number"],
textarea {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 0.9em;
}
 
textarea {
  resize: vertical;
  min-height: 80px; /* Smaller minimum height */
}
 
/* Buttons styling */
.modal-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 8px; /* Smaller gap */
}
 
button {
  padding: 8px 14px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 0.9em;
  transition: background-color 0.3s;
}
 
button[type="submit"] {
  background-color: #007bff;
  color: #fff;
}
 
button[type="submit"]:hover {
  background-color: #0056b3;
}
 
button[type="button"] {
  background-color: #6c757d;
  color: #fff;
}
 
button[type="button"]:hover {
  background-color: #5a6268;
}
 
/* Responsive design */
@media (max-width: 600px) {
  .modal-content {
    width: 100%;
    padding: 10px;
  }
}


</style>