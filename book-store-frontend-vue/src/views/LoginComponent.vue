<!-- <template>
    <div>
        <HeaderComponent/>
        Welcome to Login Page
        
    </div>
</template>

<script>

import HeaderComponent from '@/components/HeaderComponent.vue';

export default{
    name : 'LoginComponent',
    components : {
        HeaderComponent,
        
    }
}

</script>

<style>
</style> -->

<!-- 
<template>
    <div class="login-container">
      <div class="login-content">
        <h1>Welcome to Login Page</h1>
        <form @submit.prevent="login" class="login-form">
          <div class="form-group">
            <label for="username">Username:</label>
            <input type="text" id="username" v-model="username" required>
          </div>
          <div class="form-group">
            <label for="password">Password:</label>
            <input type="password" id="password" v-model="password" required>
          </div>
          <div class="form-group">
            <button type="submit">Login</button>
          </div>
          <div v-if="error" class="error-message">{{ error }}</div>
        </form>
      </div>
    </div>
  </template>
  
  <script>
  
  export default {
    name: 'LoginComponent',
    components: {
    },
    data() {
      return {
        username: '',
        password: '',
        error: ''
      };
    },
    methods: {
      login() {
        // Simulate login logic (replace with actual login logic)
        if (this.username === 'admin' && this.password === 'password') {
          // Successful login
          console.log('Login successful');
          // You can redirect to another page or handle success scenario here
        } else {
          // Failed login
          this.error = 'Invalid username or password';
          // Clear password field on failed login (optional)
          this.password = '';
        }
      }
    }
  }
  </script>
  
  <style scoped>
  .login-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background-color: #f0f0f0;
  }
  
  .login-content {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
    width: 300px;
  }
  
  .login-content h1 {
    text-align: center;
    margin-bottom: 20px;
  }
  
  .login-form {
    display: flex;
    flex-direction: column;
  }
  
  .form-group {
    margin-bottom: 15px;
  }
  
  label {
    font-weight: bold;
  }
  
  input[type="text"],
  input[type="password"],
  button {
    padding: 10px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  
  button {
    background-color: #007bff;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  button:hover {
    background-color: #0056b3;
  }
  
  .error-message {
    color: #dc3545;
    margin-top: 10px;
  }
  </style>
   -->

   

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
export default {
  name: 'LoginComponent',
  data() {
    return {
      email: '',
      password: '',
    };
  },
  methods: {
    login() {
      // Handle the login logic here
      console.log('Logging in with', this.email, this.password);
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