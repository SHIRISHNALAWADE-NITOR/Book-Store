<template>
  <div class="signup-container">
    <div class="signup-content">
      <h1>Registration</h1>
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
        <div class="form-group">
          <label for="dateOfBirth">Date of Birth</label>
          <input type="date" id="dateOfBirth" class="form-control" v-model="formData.dateOfBirth" required />
        </div>
        <div class="form-group">
          <label for="phoneNumber">Phone Number</label>
          <input type="tel" id="phoneNumber" class="form-control" v-model="formData.phoneNumber" required />
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
        dateOfBirth: '',
        phoneNumber: ''
      }
    };
  },
  methods: {
    async submitForm() {
      const form = this.$refs.signupForm;

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

      if (this.formData.password !== this.formData.confirmPassword) {
        Toastify({
          text: 'Passwords do not match.',
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
        return;
      }

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
  text-align: left;
  background: url('../assets/WhiteLibrary.png') no-repeat center center/cover;
  padding: 10px;
  box-sizing: border-box;
}

.signup-content {
  max-width: 500px; /* Reduced width for better fit */
  width: 100%;
  padding: 20px;
  background-color: #ffffff; /* White background for form */
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  color: #333;
  box-sizing: border-box;
  margin: 10px; /* Add margin to avoid overlap */
}

.signup-content h1 {
  font-size: 1.5rem; /* Slightly smaller title */
  font-weight: 600;
  margin-bottom: 15px;
  text-align: center;
  color: black; /* Vibrant color for the title */
}

.form-group {
  margin-bottom: 15px; /* Reduced margin */
}

.form-group label {
  display: block;
  font-weight: 500;
  margin-bottom: 5px;
  color: #555; /* Darker text color */
}

.form-control {
  width: 100%;
  padding: 10px;
  font-size: 0.875rem; /* Slightly smaller font size */
  border: 1px solid #ddd;
  border-radius: 4px;
  box-sizing: border-box;
}

.btn-primary {
  background-color: #4a90e2; /* Vibrant blue background */
  color: #fff;
  border: none;
  padding: 12px;
  font-size: 1rem;
  cursor: pointer;
  border-radius: 4px;
  text-align: center;
  display: block;
  width: 100%;
  transition: background-color 0.3s ease;
}

.btn-primary:hover {
  background-color: #357abd; /* Darker shade on hover */
}

@media (max-width: 600px) {
  .signup-content {
    max-width: 100%; /* Ensure full width on small screens */
    padding: 15px; /* Reduce padding */
  }

  .signup-content h1 {
    font-size: 1.25rem; /* Adjust title size */
  }

  .form-control {
    font-size: 0.875rem; /* Consistent font size */
  }

  .btn-primary {
    padding: 10px; /* Reduce button padding */
  }
}
</style>
