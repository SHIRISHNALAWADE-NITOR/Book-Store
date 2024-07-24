<!-- <template>
    <div>
        Welcome to Signup Page
    </div>
</template>

<script>

export default{
    name : 'SignupComponent',
    components : {
    }
}
</script>

<style>
</style> -->

<!-- <template>
    <div class="signup-container">
      <h1>Welcome to Signup Page</h1>
      <img src="../assets/BookOpeningGif.gif" class="bookgif">
      
      <form @submit.prevent="signup" class="signup-form">
        <div class="form-group">
          <label for="name">Name:</label>
          <input type="text" id="name" v-model="name" required>
        </div>
        
        <div class="form-group">
          <label for="username">Username:</label>
          <input type="text" id="username" v-model="username" required>
        </div>
        
        <div class="form-group">
          <label for="password">Password:</label>
          <input type="password" id="password" v-model="password" required>
        </div>
        
        <div class="form-group">
          <label for="confirm-password">Confirm Password:</label>
          <input type="password" id="confirm-password" v-model="confirmPassword" required>
        </div>
        
        <button type="submit">Sign Up</button>
        
        <div v-if="error" class="error-message">{{ error }}</div>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    name: 'SignupComponent',
    data() {
      return {
        name: '',
        username: '',
        password: '',
        confirmPassword: '',
        role: '',
        error: ''
      };
    },
    methods: {
      signup() {
        // Validate password match
        if (this.password !== this.confirmPassword) {
          this.error = 'Passwords do not match';
          return;
        }
        
        // Simulate signup logic (replace with actual signup logic)
        console.log('Name:', this.name);
        console.log('Username:', this.username);
        console.log('Password:', this.password);
        console.log('Role:', this.role);
        
        // Clear form fields after signup (optional)
        this.name = '';
        this.username = '';
        this.password = '';
        this.confirmPassword = '';
        this.role = '';
      }
    }
  };
  </script>
  
  <style scoped>
  .signup-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background-color: #f0f0f0;
  }
  
  .signup-form {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
    width: 300px;
  }

  .bookgif{
    width: 10%;

  }
  
  .signup-form h1 {
    text-align: center;
    margin-bottom: 20px;
  }
  
  .form-group {
    margin-bottom: 15px;
  }
  
  label {
    font-weight: bold;
  }
  
  input[type="text"],
  input[type="password"],
  select,
  button {
    padding: 10px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
    width: 100%;
    box-sizing: border-box;
  }
  
  select {
    appearance: none;
  }
  
  button {
    background-color: #007bff;
    color: #fff;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-top: 10px;
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
    <div class="signup-container">
      <div class="signup-content">
        <h1>Sign Up</h1>
        <form @submit.prevent="submitForm">
          <div class="form-group">
            <label for="name">Name</label>
            <input type="text" id="name" class="form-control" v-model="formData.name" required />
          </div>
          <div class="form-group">
            <label for="username">Username</label>
            <input type="text" id="username" class="form-control" v-model="formData.username" required />
          </div>
          <div class="form-group">
            <label for="password">Password</label>
            <input type="password" id="password" class="form-control" v-model="formData.password" required />
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
  export default {
    name: 'SignupComponent',
    data() {
      return {
        formData: {
          name: '',
          username: '',
          password: '',
          confirmPassword: ''
        }
      };
    },
    methods: {
      submitForm() {
        // Handle form submission logic here (e.g., validation, API calls)
        console.log('Form submitted:', this.formData);
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
    background-color:rgb(200, 164, 164);
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
  
