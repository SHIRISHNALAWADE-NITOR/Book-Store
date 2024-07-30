<template>
  <div class="login-container">
    <img src="../assets/girlreading.png" alt="Login Image" class="girl">
    <div class="form-container">
      <img src="../assets/PustakParadise.png" alt="Login Image" class="logoimage">
      <p class="subtitle">Login to your account</p>
      <form @submit.prevent="login">
        <div class="form-group">
          <input type="email" id="email" v-model="email" placeholder="Email address" required />
        </div>
        <div class="form-group">
          <input type="password" id="password" v-model="password" placeholder="Password" required />
        </div>
        <button type="submit">LOGIN</button>
      </form>
      <div class="links">
        <a href="#" class="forgot-password">Forgot password?</a>
        <p>
          Don't have an account? <a href="/signup">Register here</a>
        </p>
        <p class="terms">
          <a href="#">Terms of use</a>. <a href="#">Privacy policy</a>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css'; // Import Toastify CSS

export default {
  name: 'LoginComponent',
  data() {
    return {
      email: '',
      password: '',
    };
  },
  methods: {
    async login() {
      try {
        const response = await axios.post('https://localhost:7044/api/Auth/Login', {
          email: this.email,
          password: this.password,
        });
        
        // Handle successful login
        console.log('Login successful:', response.data);

        // Example: Store token
        localStorage.setItem('authToken', response.data.token);

        // Check user role and redirect
        if (response.data.roleId === 1) {
          this.$router.push('/adminview'); // Redirect to admin view
        } else {
          this.$router.push('/home'); // Redirect to default user dashboard
        }
      } catch (error) {
        // Handle error with Toastify
        console.error('Login failed:', error.response ? error.response.data : error.message);
        Toastify({
          text: error.response ? error.response.data.message : 'An error occurred during login.',
          backgroundColor: "linear-gradient(to right, #ff5f6d, #ffc371)",
          duration: 3000,
          close: true
        }).showToast();
      }
    },
  },
};
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #9b596b;
}

.image-container {
  display: none;
  /* Hide for smaller screens */
}

.image-container img {
  width: 400px;
  border-radius: 10px;
}

.form-container {
  background: #fff;
  padding: 40px 30px;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  max-width: 400px;
  width: 100%;
}

.logo {
  margin-bottom: 10px;
  font-size: 24px;
  font-weight: bold;
  text-align: center;
}

.subtitle {
  margin-bottom: 20px;
  text-align: center;
  color: #666;
}

.form-group {
  margin-bottom: 20px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ddd;
  border-radius: 5px;
}

button {
  width: 100%;
  padding: 10px;
  background: #333;
  color: #fff;
  font-size: 16px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background: #555;
}

.links {
  margin-top: 20px;
  text-align: center;
}

.links a {
  color: #007bff;
  text-decoration: none;
}

.links a:hover {
  text-decoration: underline;
}

.terms {
  margin-top: 10px;
  color: #666;
}

.forgot-password {
  display: block;
  margin-bottom: 10px;
}

.logoimage {
  height: 150px;
}
</style>