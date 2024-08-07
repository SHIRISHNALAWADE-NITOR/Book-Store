<template>
    <div class="admin-view">
        <header class="header">
            <h1>Admin Dashboard</h1>
        </header>
        <main class="main-content">
            <button @click="showAddBookForm" class="add-book-button">Add New Book</button>

            <div v-if="showForm" class="add-book-form">
                <h2>{{ editMode ? 'Edit Book' : 'Add New Book' }}</h2>
                <form @submit.prevent="editMode ? updateBook() : addBook()">
                    <label>
                        Title:
                        <input type="text" v-model="currentBook.title" required>
                    </label>
                    <label>
                        Author:
                        <input type="text" v-model="currentBook.author" required>
                    </label>
                    <label>
                        Category:
                        <input type="text" v-model="currentBook.category" required>
                    </label>
                    <label>
                        ISBN:
                        <input type="text" v-model="currentBook.isbn" required>
                    </label>
                    <label>
                        Description:
                        <input type="text" v-model="currentBook.description">
                    </label>
                    <label>
                        Pages:
                        <input type="number" v-model="currentBook.numberOfPages" required>
                    </label>
                    <label>
                        Rating:
                        <input type="number" step="0.1" v-model="currentBook.rating" required>
                    </label>
                    <label>
                        Price:
                        <input type="number" step="0.01" v-model="currentBook.price" required>
                    </label>
                    <label>
                        Image URL:
                        <input type="text" v-model="currentBook.imageUrl">
                    </label>
                    <button type="submit">{{ editMode ? 'Update Book' : 'Add Book' }}</button>
                    <button type="button" @click="hideForm">Cancel</button>
                </form>
            </div>

            <div class="book-list">
                <div v-for="book in paginatedBooks" :key="book.bookId" class="book-item">
                    <img :src="book.imageUrl" alt="Book Cover" class="book-cover" />
                    <div class="book-details">
                        <h2>{{ book.title }}</h2>
                        <p><strong>Author:</strong> {{ book.author }}</p>
                        <p><strong>Category:</strong> {{ book.category }}</p>
                    </div>
                    <div class="book-actions">
                        <button @click="editBook(book)">Edit</button>
                        
                        <button class="delete-icon" @click="confirmDelete(book.bookId)">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                class="bi bi-trash-fill" viewBox="0 0 16 16">
                                <path
                                    d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5M8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5m3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0" />
                            </svg>
                        </button>
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
            showForm: false,
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
                imageUrl: ''
            }
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
        fetchBooks() {
            fetch('http://localhost:5134/api/Book')
                .then(response => response.json())
                .then(data => {
                    this.books = data;
                    this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
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
            if (this.currentPage > 1) {
                this.currentPage--;
            }
        },
        nextPage() {
            if (this.currentPage < this.totalPages) {
                this.currentPage++;
            }
        },
        showAddBookForm() {
            this.resetForm();
            this.showForm = true;
        },
        hideForm() {
            this.showForm = false;
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
                    this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
                    this.hideForm();
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
            this.currentBook = { ...book };
            this.editMode = true;
            this.showForm = true;
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
                        this.books.splice(index, 1, data);
                    }
                    this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
                    this.hideForm();
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
                        text: "Failed to update book!",
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
                        // Handle successful deletion
                        const index = this.books.findIndex(book => book.bookId === bookId);
                        if (index !== -1) {
                            this.books.splice(index, 1);
                            this.totalPages = Math.ceil(this.books.length / this.booksPerPage);
                            this.hideForm(); // Assuming this hides the form or does other cleanup

                            Toastify({
                                text: "Book deleted successfully!",
                                duration: 3000,
                                close: true,
                                gravity: "top",
                                position: "right",
                                backgroundColor: "#dc3545",
                            }).showToast();
                        } else {
                            throw new Error("Book not found in local list");
                        }
                    } else {
                        return response.json().then(errorData => {
                            console.error('API Error:', errorData); // Log error data for debugging
                            throw new Error(`Error ${response.status}: ${errorData.message || 'Unknown error'}`);
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
}
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
    flex: 1;
}

.book-details h2 {
    margin: 0;
    font-size: 1.25rem;
}

.book-details p {
    margin: 0.5rem 0;
    color: #495057;
}

.book-actions {
    display: grid;
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
    background-color: #e9ecef;
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
</style>