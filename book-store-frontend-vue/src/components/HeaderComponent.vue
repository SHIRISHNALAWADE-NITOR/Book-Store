<template>
  <header class="header">
    <div class="container">
      <img src="../assets/PustakParadiselogo.png" class="brandlogo" alt="Pustak Paradise Logo">
      <div class="logo">
        <h1>Pustak Paradise</h1>
      </div>
      <nav class="nav">
        <ul>
          <li><a href="/">Home</a></li>
          <li><a href="/books">Books</a></li>
          <!-- Conditionally render Login or Logout based on auth status -->
          <li>
            <a v-if="!isLoggedIn" href="/login">Login</a>
            <a v-else href="#" @click.prevent="logout">Logout</a>
          </li>
        </ul>
      </nav>
    </div>
  </header>
</template>

<script>

export default {
  name: 'HeaderComponent',
  data() {
    return {
      isLoggedIn: false
    };
  },
  created() {
    this.checkAuthStatus();
  },
  methods: {
    checkAuthStatus() {
      // Check if the user is logged in by looking for a token in localStorage
      this.isLoggedIn = !!localStorage.getItem('authToken');
    },
    logout() {
      // Clear the token and redirect to the login page
      localStorage.removeItem('authToken');
      this.isLoggedIn = false;
      this.$router.push('/login'); // Redirect to login page or home
    }
  }
};
</script>

<style scoped>
.header {
  background-color: #00050b;
  padding: 10px 0;
  box-shadow: #00050b;
  margin-top: -61px;
}

.container {
  display: flex;
  align-items: center;
  justify-content: space-between;
  /* Distributes space between logo and navigation */
  max-width: 100%;
  margin: 0 auto;
  padding: 0 2%;
}

.logo {
  display: flex;
  align-items: center;
}

.logo h1 {
  font-size: 24px;
  color: #fff;
  margin: 0;
  margin-left: 10px;
}

.nav {
  margin-left: auto;
  /* Pushes the navigation to the right side */
}

.nav ul {
  list-style: none;
  display: flex;
  gap: 20px;
  margin: 0;
  padding: 0;
}

.nav a {
  text-decoration: none;
  color: #fff;
  font-weight: bold;
  transition: color 0.3s ease;
}

.nav a:hover {
  color: #007bff;
}

.brandlogo {
  width: 80px;
}

/* Media query for responsiveness */
@media (max-width: 768px) {
  .container {
    padding: 0 5%;
  }

  .logo h1 {
    font-size: 20px;
  }

  .nav ul {
    flex-direction: column;
  }

  .nav li {
    margin-bottom: 10px;
  }
}
</style>
