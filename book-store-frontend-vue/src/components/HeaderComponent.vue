<template>
    <header class="header">
      <div class="container">
        <div class="logo">
          <h1>Pustak Paradise</h1>
        </div>
        <nav class="nav">
          <ul>
            <li><a href="/">Home</a></li>
            <li><a href="/books">Books</a></li>
            <li><a href="/signup">Sign up</a></li>
            <li><a href="/login">Login</a></li>
          </ul>
        </nav>
      </div>
    </header>
  </template>
  
  <script>
  export default {
    name: 'HeaderComponent'
  }
  </script>
  
  <style scoped>
  .header {
    background-color: #f8f9fa;
    padding: 10px 0;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  .container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 20px;
  }
  
  .logo h1 {
    font-size: 24px;
    color: #333;
    margin: 0;
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
    color: #333;
    font-weight: bold;
  }
  
  .nav a:hover {
    color: #007bff;
  }
  </style>
  