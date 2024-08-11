<template>
    <div class="slider">
        <div class="slider-container">
            <div v-for="book in books" :key="book.bookId" class="card" @click="handleClick(book)">
                <div class="card-icon">
                    <img :src="book.imageUrl" :alt="book.title">
                </div>
                <div class="card-content">
                    
                    <p>{{ book.author }}</p>
                </div>
            </div>

            <!-- Duplicate cards for continuous sliding effect -->
            <div v-for="book in books" :key="'duplicate-' + book.bookId" class="card" @click="handleClick(book)">
                <div class="card-icon">
                    <img :src="book.imageUrl" :alt="book.title">
                </div>
                <div class="card-content">
                    
                    <p>{{ book.author }}</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';

export default {
    name: 'NewArrivals',
    setup() {
        const books = ref([]);
        const router = useRouter();

        const fetchBooks = async () => {
            try {
                const response = await fetch('http://localhost:5134/api/Book/year/2024');
                const data = await response.json();
                console.log('Fetched data:', data);
                books.value = data;
            } catch (error) {
                console.error('Error fetching books:', error);
            }
        };

        onMounted(fetchBooks);

        const handleClick = (book) => {
            router.push({ name: 'ProductDetail', params: { id: book.bookId } });
        };

        return {
            books,
            handleClick
        };
    }
};
</script>

<style scoped>

.slider {
    overflow: hidden;
    position: relative;
    width: 100%;
    height: 400px; 
}

.slider-wrapper {
    display: flex;
    /* This wrapper ensures the slider-container doesn't overflow */
    overflow: hidden;
    width: 100%;
}

.slider-container {
    display: flex;
    animation: slide 150s linear infinite; /* Adjusted animation speed */
    width: calc(200px * 20 * 2 + 20px * 20); /* Adjust width calculation to include margin */
}

.card {
    width: 200px; /* Width of each card */
    height: 400px; /* Height of each card */
    margin: 0 10px; /* Margin between cards */
    cursor: pointer;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    overflow: hidden;
}

.card:hover {
    transform: translateY(-7px);
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.3);
}
.slider:hover .slider-container {
    /* Pause the animation when hovering over the slider */
    animation-play-state: paused;
}

.card-icon {
    width: 100%;
    height: 80%;
    display: flex;
    align-items: center;
    justify-content: center;
    overflow: hidden;
    background-color: #f8f8f8;
}

.card-icon img {
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.card-content {
    width: 100%;
    height: 20%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 16px;
    font-weight: 600;
    text-align: center;
    color: #333;
    background-color: #fff;
    border-top: 1px solid #ddd;
    box-sizing: border-box;
    padding: 5px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

@keyframes slide {
    
    0% {
        transform: translateX(-50%);
    }
}

@media (max-width: 768px) {
    .card {
        width: 150px;
        height: 300px;
        margin: 0 5px;
    }
    
    .card-icon {
        height: 70%;
    }
    
    .card-content {
        font-size: 14px;
    }
}

</style>
