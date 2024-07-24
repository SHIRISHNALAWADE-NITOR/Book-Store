<template>
    <footer class="footer">
      <div class="container">
        <div class="footer-content">
          <p>&copy; 2024 Pustak Paradise. All rights reserved.</p>
          <nav class="footer-nav">
            <ul>
              <li><a href="/privacy">Privacy Policy</a></li>
              <li><a href="/terms">Terms of Service</a></li>
              <li><a href="/contact">Contact</a></li>
              <li><a href="/about">About Us</a></li>
            </ul>
          </nav>
        </div>
      </div>
    </footer>
  </template>
  
  <script>
  export default {
    name: 'FooterComponent'
  }
  </script>
  
  <style scoped>
  .footer {
    /* margin-top: 100%; */
    background-color: #343a40;
    color: #fff;
    padding: 20px 0;
    text-align: center;
    margin-bottom: 0px;
    position: relative;
    left: 0;
    bottom: 0;
    padding-top: 5px;
    width: 100%;
  }
  
  .container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 20px;
  }
  
  .footer-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    
  }
  
  .footer-nav ul {
    list-style: none;
    display: flex;
    gap: 20px;
    margin: 0;
    padding: 0;
  }
  
  .footer-nav a {
    text-decoration: none;
    color: #fff;
  }
  
  .footer-nav a:hover {
    color: #007bff;
  }
  </style>
  


