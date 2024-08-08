<template>
  <div class="about-container">
    <h1>Welcome to Pustak Paradise!</h1>

    <section class="intro">
      <p>
        At Pustak Paradise, our mission is simple: to ignite a love for reading and make every book lover’s dream come
        true. Founded with a passion for literature and a commitment to exceptional service, we offer a curated
        selection of books that cater to every reader’s taste. Whether you’re searching for the latest bestsellers,
        timeless classics, or hidden gems, you’ll find it all here in our virtual bookstore.
      </p>
    </section>

    <section class="story">
      <h2>Our Story</h2>
      <p>
        Pustak Paradise was born from a love for books and a desire to create a community where literature thrives. Our
        founders Mohit , Ritu , Shantunu and Shirish envisioned a place where readers could easily find and enjoy their
        favorite books while discovering new ones. Since our launch, we’ve grown from a small bookstore into a go-to
        online destination for book enthusiasts all over India.
      </p>
    </section>

    <section class="unique-features">
      <h2>What Sets Us Apart</h2>
      <ul>
        <li><strong>Diverse Collection:</strong> From gripping thrillers and heartwarming romances to educational texts
          and indie author spotlights, our extensive catalog caters to all genres and preferences.</li>
        <li><strong>Exclusive Editions:</strong> Enjoy access to rare editions, signed copies, and limited releases that
          you won’t find elsewhere.</li>
        <li><strong>Personalized Recommendations:</strong> Our expert team is dedicated to providing tailored book
          suggestions to help you discover your next great read.</li>
      </ul>
    </section>

    <section class="meet-team">
      <h2>Meet the Team</h2>
      <p>
        Our dedicated team is passionate about books and committed to ensuring you have the best possible shopping
        experience. [Key Team Members’ Names and Brief Bios] are here to assist with any inquiries and offer
        personalized support.
      </p>
    </section>

    <section class="commitment">
      <h2>Our Commitment</h2>
      <ul>
        <li><strong>Customer Satisfaction:</strong> Our user-friendly website and responsive customer service are
          designed to make your shopping experience smooth and enjoyable.</li>
        <li><strong>Sustainability:</strong> We’re taking steps to reduce our environmental impact with eco-friendly
          packaging and sustainable practices.</li>
        <li><strong>Diversity and Inclusion:</strong> We strive to promote diverse voices and inclusive content,
          ensuring our collection reflects a wide range of perspectives.</li>
      </ul>
    </section>

    <section class="community">
      <h2>Join Our Community</h2>
      <p>
        We love engaging with our readers! Follow us on social media, sign up for our newsletter, and stay updated on
        new arrivals, exclusive offers, and upcoming events. Share your reading adventures with us and be part of the
        Pustak Paradise family.
      </p>
    </section>

    <section class="contact">
      <h2>Contact Us</h2>
      <p>
        Have questions or need assistance? Don’t hesitate to reach out! Visit our <a href="/contact">Contact Page</a> or
        email us at <a href="mailto:contact@pustakparadise.com">contact@pustakparadise.com</a>. We’re here to help and
        ensure you have a delightful experience with us.
      </p>
    </section>
  </div>
</template>

<script>
export default {
  name: 'AboutView',
  components: {}
}
</script>

<style scoped>
.about-container {
  padding: 30px;
  max-width: 1200px;
  margin: auto;
  font-family: 'Helvetica Neue', Arial, sans-serif;
  background: linear-gradient(to right, #ffffff, #f4f4f4);
  border-radius: 12px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

h1 {
  text-align: center;
  color: #222;
  font-size: 2.8rem;
  margin-bottom: 30px;
  font-weight: 700;
}

section {
  margin-bottom: 40px;
}

h2 {
  color: #444;
  font-size: 2rem;
  margin-bottom: 15px;
  font-weight: 600;
  border-bottom: 2px solid #ddd;
  padding-bottom: 10px;
}

p {
  line-height: 1.7;
  color: #555;
  margin-bottom: 20px;
}

ul {
  list-style-type: disc;
  padding-left: 20px;
  margin: 10px 0;
}

li {
  margin-bottom: 15px;
}

a {
  color: #007BFF;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.3s ease;
}

a:hover {
  color: #0056b3;
  text-decoration: underline;
}

.intro, .story, .unique-features, .meet-team, .commitment, .community, .contact {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.unique-features ul, .commitment ul {
  list-style-type: disc;
  padding-left: 20px;
  margin: 10px 0;
}

.contact a {
  display: inline-block;
  margin-top: 10px;
}

.contact a:hover {
  text-decoration: underline;
}
</style>
