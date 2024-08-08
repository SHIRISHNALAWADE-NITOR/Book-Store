<template>
    <div class="container">
        <!-- Left Box: Order Summary -->
        <div class="box order-summary">
            <h1>Order Summary</h1>
            <div v-if="cartItems.length">
                <ul>
                    <li v-for="item in cartItems" :key="item.cartItemId">
                        <h3>{{ item.book.title }}</h3>
                        <p><strong>Quantity:</strong> {{ item.quantity }}</p>
                        <p><strong>Price:</strong> {{ item.book.price.toFixed(2) }}</p>
                        <p><strong>Total Price for Item:</strong> {{ (item.book.price * item.quantity).toFixed(2) }}</p>
                    </li>
                </ul>
                <p><strong>Total Quantity:</strong> {{ totalQuantity }}</p>
                <p><strong>Total Price:</strong> {{ totalPrice.toFixed(2) }}</p>
            </div>
            <p v-else>No items in the order.</p>
        </div>

        <!-- Right Box: Address and Payment -->
        <div class="box address-payment">
            <div class="address-selection">
                <h2>Select Address</h2>
                <select v-model="selectedAddress">
                    <option v-for="address in addresses" :key="address.id" :value="address">
                        {{ address.street }}, {{ address.city }}, {{ address.state }} - {{ address.zipCode }}
                    </option>
                </select>
                <p v-if="selectedAddress">
                    <strong>Selected Address:</strong> {{ selectedAddress.addressId }} {{ selectedAddress.street }}, {{ selectedAddress.city }}, {{ selectedAddress.state }} - {{ selectedAddress.zipCode }}
                </p>
            </div>

            <div class="pay-now">
                <button @click="showPaymentModal = true" :disabled="!selectedAddress">Pay Now</button>
            </div>
        </div>
    </div>

    <!-- Payment Modal -->
    <div v-if="showPaymentModal" class="payment-modal">
        <div class="modal-content">
            <span class="close" @click="showPaymentModal = false">&times;</span>
            <h2>Payment Information</h2>
            <form @submit.prevent="buyNow">
                <div class="form-group">
                    <label for="cardNumber">Card Number</label>
                    <input id="cardNumber" v-model="payment.cardNumber" type="text" placeholder="Enter your card number" maxlength="16" required />
                    <span v-if="errors.cardNumber" class="error">{{ errors.cardNumber }}</span>
                </div>
                <div class="form-group">
                    <label for="expiryDate">Expiry Date (MM/YY)</label>
                    <input id="expiryDate" v-model="payment.expiryDate" type="text" placeholder="MM/YY" maxlength="5" required />
                    <span v-if="errors.expiryDate" class="error">{{ errors.expiryDate }}</span>
                </div>
                <div class="form-group">
                    <label for="cvv">CVV</label>
                    <input id="cvv" v-model="payment.cvv" type="password" placeholder="Enter CVV" maxlength="3" required />
                    <span v-if="errors.cvv" class="error">{{ errors.cvv }}</span>
                </div>
                <button type="submit">Buy Now</button>
            </form>
        </div>
    </div>
</template>


<script>
import axios from 'axios';

export default {
    name: 'OrderSummaryView',
    data() {
        return {
            cartItems: [],
            addresses: [],
            selectedAddress: null,
            showPaymentModal: false,
            payment: {
                cardNumber: '',
                expiryDate: '',
                cvv: ''
            },
            errors: {}
        };
    },
    computed: {
        totalQuantity() {
            return this.cartItems.reduce((sum, item) => sum + item.quantity, 0);
        },
        totalPrice() {
            return this.cartItems.reduce((sum, item) => sum + (item.book.price * item.quantity), 0);
        }
    },
    async created() {
        const query = new URLSearchParams(this.$route.query);
        const cartItemsParam = query.get('cartItems');
        if (cartItemsParam) {
            try {
                this.cartItems = JSON.parse(decodeURIComponent(cartItemsParam));
                console.log('Parsed Cart Items:', this.cartItems);
            } catch (e) {
                console.error('Failed to parse cart items:', e);
            }
        }

        try {
            const userId = localStorage.getItem('userid');
            if (userId) {
                const response = await axios.get(`http://localhost:5134/api/Address/${userId}`);
                this.addresses = response.data;
                if (this.addresses.length > 0) {
                    this.selectedAddress = this.addresses[0]; 
                    
                }
            }
        } catch (error) {
            console.error('Error fetching addresses:', error);
        }
    },
    methods: {
        validatePayment() {
            this.errors = {};
            let isValid = true;

            if (!/^\d{16}$/.test(this.payment.cardNumber)) {
                this.errors.cardNumber = 'Card number must be 16 digits.';
                isValid = false;
            }
            if (!/^(0[1-9]|1[0-2])\/\d{2}$/.test(this.payment.expiryDate)) {
                this.errors.expiryDate = 'Expiry date must be in MM/YY format.';
                isValid = false;
            }
            if (!/^\d{3}$/.test(this.payment.cvv)) {
                this.errors.cvv = 'CVV must be 3 digits.';
                isValid = false;
            }

            return isValid;
        },
        async buyNow() {
            if (!this.validatePayment()) {
                return;
            }

            const orderData = {
                orderId: 0,
                userId: parseInt(localStorage.getItem('userid'), 10),
                orderDate: new Date().toISOString(),
                totalAmount: this.totalPrice,
                shippingAddressId: this.selectedAddress.addressId,
                orderItems: this.cartItems.map(item => ({
                    orderItemId: 0,
                    bookId: item.book.bookId,
                    quantity: item.quantity,
                    price: item.book.price
                }))
            };
            console.log(this.selectedAddress.id);
            
            try {
                const response = await axios.post('http://localhost:5134/api/Order', orderData);
                console.log('Order successful:', response.data);
                await axios.delete(`http://localhost:5134/api/CartItem/user/${orderData.userId}`);
                this.showPaymentModal = false;

                // Optionally, redirect or show a success message
                this.$router.push('/order-success');
            } catch (error) {
                console.error('Error creating order:', error);
                // Handle the error (e.g., show a message to the user)
            }
        }
    }
};
</script>

<style scoped>
/* Container to hold both boxes side by side */
.container {
    display: flex;
    justify-content: space-between; /* Ensures space between the boxes */
    gap: 20px; /* Adjust spacing between boxes */
    text-align: left;
}

/* Common box styling */
.box {
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    width: 48%; /* Adjust width to fit side-by-side layout */
}

/* Specific styling for order summary box */
.order-summary {
    background-color: #f9f9f9;
}

/* Specific styling for address and payment box */
.address-payment {
    background-color: #f4f4f4;
}

/* Address selection styling */
.address-selection {
    margin-bottom: 20px;
}

/* Pay Now button styling */
button {
    padding: 12px 25px;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 1rem;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: #45a049;
}

button:disabled {
    background-color: #ddd;
    cursor: not-allowed;
}

/* Modal styles */
.payment-modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.6);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal-content {
    background: white;
    padding: 30px;
    border-radius: 8px;
    width: 90%;
    max-width: 600px;
    position: relative;
}

.close {
    position: absolute;
    top: 10px;
    right: 10px;
    font-size: 1.5rem;
    color: #333;
    cursor: pointer;
}

.close:hover {
    color: #ff0000;
}

/* Form styling */
.form-group {
    margin-bottom: 20px;
}

label {
    display: block;
    margin-bottom: 5px;
    font-weight: bold;
    color: #333;
}

input {
    width: 100%;
    padding: 10px;
    border: 1px solid #ddd;
    border-radius: 4px;
    box-sizing: border-box;
    font-size: 1rem;
}

.error {
    color: #e74c3c;
    font-size: 0.8rem;
    margin-top: 5px;
}
</style>



