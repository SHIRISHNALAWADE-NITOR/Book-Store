<template>
    <div class="forgot-password">
        <h1>Forgot Password</h1>
        <form @submit.prevent="handleSubmit">
            <div>
                <label for="email">Email:</label>
                <input type="email" id="email" v-model="email" required />
            </div>
            <div>
                <label for="dob">Date of Birth:</label>
                <input type="date" id="dob" v-model="dob" required />
            </div>
            <button type="submit">Submit</button>
        </form>
        <p v-if="message">{{ message }}</p>
    </div>
</template>

<script>
import axios from 'axios';
//import { useRouter } from 'vue-router';

export default {
    name: 'ForgotPassword',
    data() {
        return {
            email: '',
            dob: '',
            message: ''
        };
    },
    methods: {
        async handleSubmit() {
            try {
                //const formattedDob = new Date(this.dob).toISOString();
                const response = await axios.post('http://localhost:5134/api/Auth/ForgotPasswordEmail', {
                    email: this.email,
                    dob: this.dob
                });

                // Extract relevant details from the response
                const { token } = response.data;

                // Store or pass the token, email, and dob to the next route
                this.$router.push({
                    name: 'ResetPassword',
                    query: { email: this.email, token, dob: this.dob}
                });

            } catch (error) {
                this.message = 'An error occurred. Please try again.';
            }
        }
    }
}
</script>

<style scoped>
.forgot-password {
    max-width: 400px;
    margin: 0 auto;
    padding: 1em;
    border: 1px solid #ddd;
    border-radius: 8px;
}

form div {
    margin-bottom: 1em;
}

label {
    margin-bottom: .5em;
    color: #333333;
    display: block;
}

input {
    border: 1px solid #CCCCCC;
    padding: .5em;
    font-size: 1em;
    width: 100%;
}

button {
    padding: 0.7em;
    border: none;
    background-color: #007bff;
    color: white;
    font-size: 1em;
    cursor: pointer;
}

button:hover {
    background-color: #0056b3;
}

p {
    color: #28a745;
}
</style>