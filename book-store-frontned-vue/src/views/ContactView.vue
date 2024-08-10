<template>
    <div class="contact-container">
        <h1>Contact Us</h1>

        <section class="contact-info">
            <h2>Get in Touch</h2>
            <p>
                We’re here to help! Whether you have questions, need support, or just want to say hello, feel free to
                reach out to us using the contact information below or by filling out the form.
            </p>

            <div class="contact-details">
                <div class="contact-item">
                    <h3>Email Us</h3>
                    <p>
                        <a href="mailto:contact@pustakparadise.com">contact@pustakparadise.com</a>
                    </p>
                </div>

                <div class="contact-item">
                    <h3>Call Us</h3>
                    <p>
                        <a href="tel:+1234567890">+1 (234) 567-890</a>
                    </p>
                </div>

                <div class="contact-item">
                    <h3>Follow Us</h3>
                    <p>
                        Connect with us on social media:
                        <a href="https://facebook.com/pustakparadise" target="_blank" rel="noopener">Facebook</a>,
                        <a href="https://twitter.com/pustakparadise" target="_blank" rel="noopener">Twitter</a>,
                        <a href="https://instagram.com/pustakparadise" target="_blank" rel="noopener">Instagram</a>.
                    </p>
                </div>

                <!-- Add a map if applicable -->
                <div class="contact-item map">
                    <h3>Our Location</h3>
                    <iframe
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3723.876738839375!2d73.7402006!3d18.5879446!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3bc2bb8262560ba3%3A0x3d895ee17fa37505!2sVeera%20Sai%20PG!5e0!3m2!1sen!2sus!4v1695808286207!5m2!1sen!2sus"
                        width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy">
                    </iframe>
                </div>

            </div>
        </section>

        <section class="contact-form">
            <h2>Contact Form</h2>
            <form @submit.prevent="handleSubmit">
                <div class="form-group">
                    <label for="name">Name</label>
                    <input type="text" id="name" v-model="form.name" required>
                </div>

                <div class="form-group">
                    <label for="email">Email</label>
                    <input type="email" id="email" v-model="form.email" required>
                </div>

                <div class="form-group">
                    <label for="message">Message</label>
                    <textarea id="message" v-model="form.message" rows="4" required></textarea>
                </div>

                <button type="submit">Send Message</button>
            </form>
        </section>
    </div>
</template>

<script>
export default {
    name: 'ContactView',
    data() {
        return {
            form: {
                name: '',
                email: '',
                message: ''
            }
        };
    },
    methods: {
        handleSubmit() {
            // Handle form submission logic here (e.g., send email, call API)
            alert('Thank you for your message. We will get back to you soon!');
            // Clear form fields
            this.form.name = '';
            this.form.email = '';
            this.form.message = '';
        }
    }
}
</script>

<style scoped>
.contact-container {
  padding: 30px;
  max-width: 1000px;
  margin: 20px auto; /* Centers the container horizontally */
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: #f9f9f9;
  border-radius: 12px;
  text-align: left;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  color: #222;
  font-size: 2.8rem;
  margin-bottom: 40px;
  font-weight: 700;
}

section {
  margin-bottom: 40px;
}

.contact-info h2,
.contact-form h2 {
  color: #444;
  font-size: 2rem;
  margin-bottom: 15px;
  font-weight: 600;
  text-align: left;
}

.contact-details {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.contact-item {
  background-color: #ffffff;
  border-radius: 10px;
  padding: 20px;
  border: 1px solid #e0e0e0;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: box-shadow 0.3s ease, transform 0.3s ease;
}

.contact-item:hover {
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
  transform: translateY(-5px);
}

.contact-item h3 {
  color: #333;
  font-size: 1.6rem;
  margin-bottom: 10px;
}

.contact-item p {
  color: #555;
  line-height: 1.7;
}

.contact-item a {
  color: #007BFF;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.3s ease;
}

.contact-item a:hover {
  color: #0056b3;
}

.map iframe {
  width: 100%;
  height: 300px;
  border: none;
  border-radius: 10px;
  margin-top: 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.contact-form {
  padding: 25px;
  background-color: #ffffff;
  border-radius: 10px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 20px;
}

.form-group label {
  display: block;
  font-weight: 500;
  margin-bottom: 8px;
  color: #333;
}

.form-group input,
.form-group textarea {
  width: 100%;
  padding: 14px;
  border: 1px solid #ddd;
  border-radius: 6px;
  box-sizing: border-box;
  font-size: 1rem;
  background-color: #f9f9f9;
  transition: border-color 0.3s ease, background-color 0.3s ease;
}

.form-group input:focus,
.form-group textarea:focus {
  border-color: #007BFF;
  background-color: #ffffff;
  outline: none;
}

.form-group textarea {
  resize: vertical;
}

button {
  display: inline-block;
  padding: 14px 28px;
  font-size: 1.2rem;
  color: #ffffff;
  background-color: #007BFF;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
  font-weight: 600;
}

button:hover {
  background-color: #0056b3;
  transform: translateY(-2px);
}

button:active {
  transform: translateY(0);
}
</style>
