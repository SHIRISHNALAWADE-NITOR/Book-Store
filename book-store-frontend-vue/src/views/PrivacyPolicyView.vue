<template>
  <div class="privacy-policy">
    <h1>Privacy Policy</h1>
    <p>Welcome to Pustak Paradise! This Privacy Policy outlines how we collect, use, and protect your personal
      information when you visit our website.</p>

    <section>
      <h2>Introduction</h2>
      <p>At Pustak Paradise, your privacy is important to us. We are committed to protecting your personal information
        and ensuring that your experience with us is safe and enjoyable.</p>
    </section>

    <section>
      <h2>Information We Collect</h2>
      <p>We collect various types of information to provide you with the best service possible. This includes:</p>
      <ul>
        <li>Personal information you provide (e.g., name, email address, phone number)</li>
        <li>Purchase history and preferences</li>
        <li>Technical information about your device and browsing behavior (e.g., IP address, browser type, and cookies)
        </li>
      </ul>
    </section>

    <section>
      <h2>How We Use Your Information</h2>
      <p>We use your information to:</p>
      <ul>
        <li>Process your orders and manage your account</li>
        <li>Improve our website and services</li>
        <li>Send you promotional materials and updates (you can opt out at any time)</li>
        <li>Respond to your inquiries and provide customer support</li>
      </ul>
    </section>

    <section>
      <h2>Cookies</h2>
      <p>We use cookies to enhance your browsing experience. Cookies help us remember your preferences and track website
        usage. You can control cookie settings through your browser.</p>
    </section>

    <section>
      <h2>Third-Party Services</h2>
      <p>Our website may contain links to third-party sites. We are not responsible for the privacy practices or content
        of these sites. Please review their privacy policies separately.</p>
    </section>

    <section>
      <h2>Data Security</h2>
      <p>We take reasonable measures to protect your personal information from unauthorized access, disclosure, or
        alteration. However, no method of transmission over the internet or electronic storage is 100% secure.</p>
    </section>

    <section>
      <h2>Your Rights</h2>
      <p>You have the right to access, correct, or delete your personal information. You can also object to or restrict
        certain processing activities. To exercise these rights, please contact us.</p>
    </section>

    <section>
      <h2>Changes to This Policy</h2>
      <p>We may update this Privacy Policy from time to time. We will notify you of any significant changes by posting
        the new policy on our website. Please review this policy periodically for any updates.</p>
    </section>

    <section>
      <h2>Contact Us</h2>
      <p>If you have any questions or concerns about our Privacy Policy, please contact us at:</p>
      <p>Email: support@pustakparadise.com</p>
      <p>Address: 123 Book Street, Readsville, BK 45678</p>
    </section>
  </div>
</template>

<script>
export default {
  name: 'PrivacyPolicyView',
}
</script>

<style scoped>
.privacy-policy {
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background-color: #f9f9f9;
  color: #333;
  max-width: 800px;
  margin: 20px auto; /* Centers the container, but not the text */
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  text-align: left;
}

.privacy-policy h1 {
  font-size: 28px;
  margin-bottom: 20px;
  color: #2c3e50;
  border-bottom: 2px solid #3498db;
  padding-bottom: 10px;
}

.privacy-policy h2 {
  font-size: 22px;
  margin-top: 30px;
  margin-bottom: 15px;
  color: #3498db;
  border-bottom: 1px solid #ddd;
  padding-bottom: 5px;
}

.privacy-policy p {
  font-size: 16px;
  line-height: 1.6;
  margin-bottom: 20px;
  text-align: left; /* Ensure text alignment is left */
}

.privacy-policy ul {
  list-style-type: disc;
  margin-left: 20px;
}

.privacy-policy li {
  margin-bottom: 12px;
}

.privacy-policy a {
  color: #3498db;
  text-decoration: none;
}

.privacy-policy a:hover {
  text-decoration: underline;
}

.privacy-policy section {
  margin-bottom: 40px;
}
</style>
