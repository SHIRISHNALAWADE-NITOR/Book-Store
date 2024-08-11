<template>
    <div class="slider">
        <div class="slider-container">
            <div v-for="category in categories" :key="category.category" class="card" @click="handleClick(category)">
                <div class="card-icon">
                    <img :src="category.imageUrl" :alt="category.category">
                </div>
                <div class="card-content">
                    {{ category.category }}
                </div>
            </div>

            <div v-for="category in categories" :key="'duplicate-' + category.category" class="card">
                <div class="card-icon">
                    <img :src="category.imageUrl" :alt="category.category">
                </div>
                <div class="card-content">
                    {{ category.category }}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';

export default {
    name: 'SlidingCards',
    setup() {
        const categories = ref([]);
        const router = useRouter();

        const fetchCategories = async () => {
            try {
                const response = await fetch('http://localhost:5134/api/Book/category/topbooks');
                const data = await response.json();
                console.log('Fetched data:', data);
                categories.value = data;
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        onMounted(fetchCategories);

        const handleClick = (category) => {
            router.push({ name: 'Books', query: { category: category.category } });
        };

        return {
            categories,
            handleClick
        };
    }
};
</script>

<style scoped>
.slider {
    overflow: hidden;
    white-space: nowrap;
    position: relative;
    width: 100%;
    height: 400px;
    
}


.slider-container {
    display: flex;
    animation: slide 100s linear infinite;
    /* Adjusted animation speed */
    width: calc(200px * 20 * 2);
    /* Width for narrower cards */
    
}


.card {
    width: 200px;
    /* Narrower width for tall cards */
    height: 400px;
    /* Increased height for tall cards */
    margin: 0 10px;
    /* Reduced margin between cards */
    cursor: pointer;
    display: flex;
    flex-direction: column;
    /* Stack image and text vertically */
    align-items: center;
    justify-content: center;
    background-color: #fff;
    /* Background color for the card */
    border-radius: 8px;
    /* Border radius for rounded corners */
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
    /* Shadow for card depth */
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    /* Smooth transitions */
    overflow: hidden;
    /* Ensure content does not overflow the card */
}

.card:hover {
    transform: translateY(-7px);
    /* Lift card on hover */
    box-shadow: 0 16px 32px rgba(0, 0, 0, 0.3);
    /* Enhanced shadow on hover */
    
}
.slider:hover .slider-container {
    /* Pause the animation when hovering over the slider */
    animation-play-state: paused;
}

.card-icon {
    width: 100%;
    /* Full width of the card */
    height: 80%;
    /* Increased height to cover more space */
    display: flex;
    align-items: center;
    justify-content: center;
    overflow: hidden;
    /* Hide overflow of images */
    background-color: #f8f8f8;
    /* Background color for icon area */
}

.card-icon img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    /* Cover the entire area of the card */
}

.card-content {
    width: 100%;
    /* Full width of the card */
    height: 20%;
    /* Reduced height for the text area */
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 16px;
    /* Slightly smaller font size */
    font-weight: 600;
    /* Font weight for emphasis */
    text-align: center;
    /* Center text */
    color: #333;
    /* Text color */
    background-color: #fff;
    /* Background color for text area */
    border-top: 1px solid #ddd;
    /* Border for separation */
    box-sizing: border-box;
    /* Ensure padding and border are included in the height */
    padding: 5px;
    /* Reduced padding */
    overflow: hidden;
    /* Hide overflow text */
    text-overflow: ellipsis;
    /* Add ellipsis for overflowing text */
    white-space: nowrap;
    /* Prevent text from wrapping */
}

@keyframes slide {
    from {
        transform: translateX(0);
    }

    to {
        transform: translateX(-50%);
        /* Adjusted to fit the container size */
    }
}

@media (max-width: 768px) {
    .card {
        width: 150px;
        /* Adjusted width for smaller screens */
        height: 300px;
        /* Adjusted height for smaller screens */
        margin: 0 5px;
        /* Further reduced margin for smaller screens */
    }

    .card-icon {
        height: 70%;
        /* Adjusted height for smaller screens */
    }

    .card-content {
        font-size: 14px;
        /* Further reduced font size for smaller screens */
    }
}
</style>
