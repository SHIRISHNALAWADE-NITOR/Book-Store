<template>
  <div class="signup-container">
    <div class="signup-content">
      <h1>Sign Up</h1>
      <form @submit.prevent="submitForm" ref="signupForm">
        <div class="form-group">
          <label for="name">Name</label>
          <input type="text" id="name" class="form-control" v-model="formData.name" required />
        </div>
        <div class="form-group">
          <label for="username">Username</label>
          <input type="text" id="username" class="form-control" v-model="formData.username" required />
        </div>
        <div class="form-group">
          <label for="email">Email</label>
          <input type="email" id="email" class="form-control" v-model="formData.email" required />
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <input type="password" id="password" class="form-control" v-model="formData.password" required minlength="8" />
        </div>
        <div class="form-group">
          <label for="confirm-password">Confirm Password</label>
          <input type="password" id="confirm-password" class="form-control" v-model="formData.confirmPassword" required />
        </div>
        <button type="submit" class="btn btn-primary btn-block">Sign Up</button>
      </form>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css'; // Import Toastify CSS

export default {
  name: 'SignupComponent',
  data() {
    return {
      formData: {
        userId: 0,
        roleId: 2,
        name: '',
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        dateOfBirth: '2024-07-29T10:13:00.053Z', // default value or could be updated
        phoneNumber: '' // default value or could be updated
      }
    };
  },
  methods: {
    async submitForm() {
      const form = this.$refs.signupForm;

      // Check if form is valid
      if (!form.checkValidity()) {
        Toastify({
          text: 'Please fill out all required fields correctly.',
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
        form.reportValidity(); // Triggers HTML5 validation messages
        return;
      }

      // Custom validation
      if (this.formData.password !== this.formData.confirmPassword) {
        Toastify({
          text: 'Passwords do not match.',
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
        return;
      }

      // Construct request payload
      const requestPayload = {
        userId: this.formData.userId,
        name: this.formData.name,
        roleId: this.formData.roleId,
        username: this.formData.username,
        dateOfBirth: this.formData.dateOfBirth,
        phoneNumber: this.formData.phoneNumber,
        email: this.formData.email,
        passwordHash: this.formData.password // Adjust if your backend expects a hashed password
      };

      try {
        const response = await axios.post('http://localhost:5134/api/Auth/Register', requestPayload);
        console.log('Registration successful:', response.data);
        Toastify({
          text: 'Registration successful! Please log in.',
          backgroundColor: "linear-gradient(to right, #00b09b, #96c93d)",
          duration: 3000,
          close: true
        }).showToast();
        this.$router.push('/login'); // Redirect to login page after successful registration
      } catch (error) {
        console.error('Registration failed:', error.response ? error.response.data : error.message);
        Toastify({
          text: error.response ? error.response.data.message : 'An error occurred during registration.',
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
      }
    }
  }
};
</script>

<style scoped>
.signup-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background: url('../assets/WhiteLibrary.png') no-repeat center center/cover;
}

.signup-content {
  max-width: 400px;
  width: 100%;
  padding: 30px;
  background-color: rgb(200, 164, 164);
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  color: #222;
}

.signup-content h1 {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 20px;
}

.form-group label {
  font-weight: bold;
}

.form-control {
  width: 100%;
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.btn-primary {
  background-color: #007bff;
  color: #fff;
  border: none;
  padding: 10px 20px;
  font-size: 1rem;
  cursor: pointer;
}

.btn-primary:hover {
  background-color: #0056b3;
}

.terms {
  margin-top: 20px;
  font-size: 0.875rem;
}

.terms a {
  color: #007bff;
  text-decoration: none;
}

.terms a:hover {
  text-decoration: underline;
}
</style>
