<template>
    <header class="header">
      <div class="container">
        <img src="../assets/PustakParadiselogo.png" class="brandlogo">
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
    background-color: #343a40;
    padding: 10px 0;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    margin-top: -61px;
  }
  
  .container {
    display: flex;
    align-items: center;
    max-width: 100%; /* Ensure container does not exceed viewport */
    margin: 0 auto;
    padding: 0 2%; /* Responsive padding */
  }
  
  .logo {
    display: flex; /* Ensure logo and image are aligned correctly */
    align-items: center; /* Align items vertically */
  }
  
  .logo h1 {
    font-size: 24px;
    color: #fff;
    margin: 0;
    margin-left: 10px; /* Adjust margin between image and text */
  }
  
  .nav ul {
    list-style: none;
    display: flex;
    gap: 20px;
    margin: 0;
    padding: 0;
    margin-left: 250%; /* Push navigation to the right */
  }
  
  .nav a {
    text-decoration: none;
    color: #fff;
    font-weight: bold;
    transition: color 0.3s ease; /* Smooth transition for hover effect */
  }
  
  .nav a:hover {
    color: #007bff; /* Blue color on hover */
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
      flex-direction: column; /* Stack items vertically on smaller screens */
    }
  
    .nav li {
      margin-bottom: 10px; /* Add spacing between stacked menu items */
    }
  }
  </style>
  