<template>
    <div class="order-summary">
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

        <div class="address-selection">
            <h2>Select Address</h2>
            <select v-model="selectedAddress">
                <option v-for="address in addresses" :key="address.id" :value="address">
                    {{ address.street }}, {{ address.city }}, {{ address.state }} - {{ address.zipCode }}
                </option>
            </select>
            
            <p v-if="selectedAddress">
                <strong>Selected Address:</strong>{{ selectedAddress.addressId }} {{ selectedAddress.id }} {{ selectedAddress.street }}, {{ selectedAddress.city }}, {{
                selectedAddress.state }} - {{ selectedAddress.zipCode }}
            </p>
        </div>

        <!-- Pay Now Button -->
        <div class="pay-now">
            <button @click="showPaymentModal = true" :disabled="!selectedAddress">Pay Now</button>
        </div>

        <!-- Payment Modal -->
        <div v-if="showPaymentModal" class="payment-modal">
            <div class="modal-content">
                <span class="close" @click="showPaymentModal = false">&times;</span>
                <h2>Payment Information</h2>
                <form @submit.prevent="buyNow">
                    <div class="form-group">
                        <label for="cardNumber">Card Number</label>
                        <input id="cardNumber" v-model="payment.cardNumber" type="text"
                            placeholder="Enter your card number" maxlength="16" required />
                        <span v-if="errors.cardNumber" class="error">{{ errors.cardNumber }}</span>
                    </div>
                    <div class="form-group">
                        <label for="expiryDate">Expiry Date (MM/YY)</label>
                        <input id="expiryDate" v-model="payment.expiryDate" type="text" placeholder="MM/YY"
                            maxlength="5" required />
                        <span v-if="errors.expiryDate" class="error">{{ errors.expiryDate }}</span>
                    </div>
                    <div class="form-group">
                        <label for="cvv">CVV</label>
                        <input id="cvv" v-model="payment.cvv" type="text" placeholder="Enter CVV" maxlength="3"
                            required />
                        <span v-if="errors.cvv" class="error">{{ errors.cvv }}</span>
                    </div>
                    <button type="submit">Buy Now</button>
                </form>
            </div>
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
                const response = await axios.get(`https://localhost:7044/api/Address/${userId}`);
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
                const response = await axios.post('https://localhost:7044/api/Order', orderData);
                console.log('Order successful:', response.data);
                await axios.delete(`https://localhost:7044/api/CartItem/user/${orderData.userId}`);
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
.order-summary {
    padding: 20px;
}

.address-selection {
    margin-top: 20px;
}

select {
    width: 100%;
    padding: 10px;
    margin-bottom: 10px;
}

p {
    margin-top: 10px;
}

.pay-now {
    margin-top: 20px;
}

button {
    padding: 10px 20px;
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
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
    background: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal-content {
    background: white;
    padding: 20px;
    border-radius: 8px;
    width: 80%;
    max-width: 500px;
    position: relative;
}

.close {
    position: absolute;
    top: 10px;
    right: 10px;
    font-size: 24px;
    cursor: pointer;
}

.form-group {
    margin-bottom: 15px;
}

input {
    width: 100%;
    padding: 8px;
    box-sizing: border-box;
}

.error {
    color: red;
    font-size: 12px;
}
</style>
