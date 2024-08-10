<template>
  <div class="reset-password">
    <h1>Reset Password</h1>
    <form @submit.prevent="handleReset">
      <div>
        <label for="otp">OTP:</label>
        <input type="text" id="otp" v-model="otp" required />
      </div>
      <div>
        <label for="new-password">New Password:</label>
        <input type="password" id="new-password" v-model="newPassword" required />
      </div>
      <button type="submit">Reset Password</button>
    </form>
    <p v-if="message">{{ message }}</p>
  </div>
</template>

<script>
import axios from 'axios';
import { useRoute, useRouter } from 'vue-router';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';

export default {
  name: 'ResetPassword',
  data() {
    return {
      newPassword: '',
      otp: '',
      message: ''
    };
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    return { route, router };
  },
  methods: {
    async handleReset() {
      try {
        const { email, token, dob } = this.route.query;
        await axios.post('http://localhost:5134/api/Auth/ResetPasswordEmail', {
          email: email,
          token: token,
          DateOfBirth: dob,
          newPassword: this.newPassword,
          otp: this.otp
        });

        Toastify({
          text: "Password successfully changed!",
          backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
          className: "info",
          position: "center",
          duration: 3000
        }).showToast();

        // Redirect to login page
        this.router.push('/login');
      } catch (error) {
        this.message = 'An error occurred. Please try again.';
      }
    }
  }
}
</script>

<style scoped>
/* Add CSS specific to ResetPassword.vue */
.reset-password {
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
