<template>
  <div class="profile-container">
    <div class="profile-info">
      <h1>WELCOME {{ userName }}</h1>
      <div class="user-profile" v-if="user">
        <h2>User Profile</h2>
        <p><strong>Name:</strong> {{ user.name }}</p>
        <p><strong>Email:</strong> {{ user.email }}</p>
        <p><strong>Username:</strong> {{ user.username }}</p>
        <p><strong>Phone Number:</strong> {{ user.phoneNumber }}</p>
        <p><strong>Date of Birth:</strong> {{ formattedDateOfBirth }}</p>
        <button @click="viewOrderHistory">Order History</button>
      </div>
    </div>

    <div class="address-info">
      <div class="address-section">
        <h2>Addresses</h2>
        <div v-if="addresses.length">
          <div v-for="address in addresses" :key="address.addressId" class="address-card">
            <p><strong>Street:</strong> {{ address.street }}</p>
            <p><strong>City:</strong> {{ address.city }}</p>
            <p><strong>State:</strong> {{ address.state }}</p>
            <p><strong>Postal Code:</strong> {{ address.postalCode }}</p>
            <p><strong>Country:</strong> {{ address.country }}</p>
            <button @click="editAddress(address)">Edit Address</button>
          </div>
        </div>
        <button @click="openAddDialog">Add Address</button>
      </div>
    </div>
  </div>

  <!-- Address Dialog -->
  <div v-if="showAddressDialog" class="dialog-overlay">
    <div class="dialog-content">
      <h3>{{ dialogTitle }}</h3>
      <form @submit.prevent="saveAddress">
        <div>
          <label for="street">Street:</label>
          <input type="text" v-model="addressData.street" required />
        </div>
        <div>
          <label for="city">City:</label>
          <input type="text" v-model="addressData.city" required />
        </div>
        <div>
          <label for="state">State:</label>
          <input type="text" v-model="addressData.state" required />
        </div>
        <div>
          <label for="postalCode">Postal Code:</label>
          <input type="text" v-model="addressData.postalCode" required pattern="[1-9][0-9]{5}" />
          <small v-if="!validPostalCode">Please enter a valid postal code.</small>
        </div>
        <div>
          <label for="country">Country:</label>
          <input type="text" v-model="addressData.country" required />
        </div>
        <button type="submit">Save</button>
        <button @click="closeAddressDialog" type="button">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script>
import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css';

export default {
  name: 'UserProfileVue',
  data() {
    return {
      user: null,
      addresses: [],
      showAddressDialog: false,
      editingAddress: null,
      addressData: {
        street: '',
        city: '',
        state: '',
        postalCode: '',
        country: ''
      },
      validPostalCode: true
    };
  },
  computed: {
    userName() {
      return this.user ? this.user.name.toUpperCase() : '';
    },
    formattedDateOfBirth() {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      return this.user ? new Date(this.user.dateOfBirth).toLocaleDateString(undefined, options) : '';
    },
    dialogTitle() {
      return this.editingAddress ? 'Edit Address' : 'Add Address';
    }
  },
  async created() {
    await this.fetchUserData();
    await this.fetchAddressData();
  },
  methods: {
    async fetchUserData() {
      try {
        const userId = localStorage.getItem('userid');
        if (userId) {
          const response = await fetch(`http://localhost:5134/api/Users/${userId}`);
          if (response.ok) {
            this.user = await response.json();
          } else {
            console.error('Failed to fetch user data');
          }
        }
      } catch (error) {
        console.error('Error fetching user data:', error);
      }
    },
    async fetchAddressData() {
      try {
        const userId = localStorage.getItem('userid');
        if (userId) {
          const response = await fetch(`http://localhost:5134/api/Address/${userId}`);
          if (response.ok) {
            this.addresses = await response.json();
          } else if (response.status === 404) {
            this.addresses = [];
          } else {
            console.error('Failed to fetch address data');
          }
        }
      } catch (error) {
        console.error('Error fetching address data:', error);
      }
    },
    openAddDialog() {
      this.addressData = {
        street: '',
        city: '',
        state: '',
        postalCode: '',
        country: ''
      };
      this.editingAddress = null;
      this.showAddressDialog = true;
    },
    editAddress(address) {
      this.addressData = { ...address };
      this.editingAddress = address;
      this.showAddressDialog = true;
    },
    closeAddressDialog() {
      this.showAddressDialog = false;
    },
    async saveAddress() {
      if (!this.validPostalCode) return;

      try {
        const userId = localStorage.getItem('userid');
        let response;

        if (this.editingAddress) {
          response = await fetch(`http://localhost:5134/api/Address/${this.editingAddress.addressId}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.addressData)
          });

          if (response.ok) {
            const updatedAddress = await response.json();
            const index = this.addresses.findIndex(addr => addr.addressId === this.editingAddress.addressId);
            if (index !== -1) {
              this.$set(this.addresses, index, updatedAddress);
            }
            Toastify({
              text: 'Address updated successfully!',
              backgroundColor: '#28a745',
              className: 'info',
            }).showToast();
          } else {
            console.error('Failed to update address');
          }
        } else {
          response = await fetch(`http://localhost:5134/api/Address`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify({
              ...this.addressData,
              userId: Number(userId)
            })
          });

          if (response.ok) {
            const newAddress = await response.json();
            this.addresses.push(newAddress);
            Toastify({
              text: 'Address added successfully!',
              backgroundColor: '#28a745',
              className: 'info',
            }).showToast();
          } else {
            console.error('Failed to add address');
          }
        }

        this.closeAddressDialog();
        this.$emit('address-updated'); // Notify parent component
      } catch (error) {
        console.error('Error saving address:', error);
      }
    },
    viewOrderHistory() {
      this.$router.push({ name: 'OrderHistory' }); // Navigate to Order History page
    }
  },
  watch: {
    'addressData.postalCode': function (newValue) {
      this.validPostalCode = /^[1-9][0-9]{5}$/.test(newValue);
    }
  }
};
</script>

<style scoped>
/* Profile Container */
.profile-container {
  display: flex;
  justify-content: space-between;
  gap: 20px;
  max-width: 1200px;
  margin: 40px auto;
  padding: 20px;
  text-align: left;
}

/* Profile Information */
.profile-info {
  flex: 1;
  max-width: 50%;
}

.profile-info h1 {
  font-size: 2rem;
  margin-bottom: 20px;
  color: #333333;
  text-align: left;
  font-weight: 600;
}

.user-profile {
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.user-profile h2 {
  font-size: 1.5rem;
  margin-bottom: 15px;
  color: #333333;
  font-weight: 600;
}

.user-profile p {
  font-size: 1rem;
  margin: 10px 0;
  line-height: 1.5;
  color: #666666;
}

/* Address Information */
.address-info {
  flex: 1;
  max-width: 50%;
}

.address-section {
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 20px;
  background-color: #ffffff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.address-section h2 {
  font-size: 1.5rem;
  margin-bottom: 15px;
  color: #333333;
  font-weight: 600;
}

.address-card {
  border: 1px solid #d0d0d0;
  padding: 15px;
  margin-bottom: 15px;
  border-radius: 8px;
  background-color: #f9f9f9;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.address-card p {
  margin: 5px 0;
  font-size: 1rem;
  color: #666666;
}

.address-card button {
  background-color: #007bff;
  color: #ffffff;
  padding: 8px 16px;
  font-size: 0.875rem;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s;
}

.address-card button:hover {
  background-color: #0056b3;
  transform: scale(1.02);
}

.address-section button {
  margin-top: 20px;
  background-color: #007bff;
  color: #ffffff;
  padding: 10px 20px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 0.875rem;
  transition: background-color 0.3s, transform 0.2s;
}

.address-section button:hover {
  background-color: #0056b3;
  transform: scale(1.02);
}

/* Dialog Styling */
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.dialog-content {
  background-color: #ffffff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  width: 90%;
  max-width: 400px;
  box-sizing: border-box;
}

.dialog-content h3 {
  margin-top: 0;
  margin-bottom: 15px;
  font-size: 1.25rem;
  color: #333333;
  font-weight: 600;
}

.dialog-content form {
  display: flex;
  flex-direction: column;
}

.dialog-content div {
  margin-bottom: 12px;
}

.dialog-content label {
  display: block;
  margin-bottom: 4px;
  font-weight: 600;
  color: #444444;
}

.dialog-content input {
  width: 100%;
  padding: 8px;
  border: 1px solid #d0d0d0;
  border-radius: 6px;
  font-size: 0.875rem;
  color: #333333;
}

.dialog-content small {
  color: #dc3545;
  font-size: 0.75rem;
  margin-top: 4px;
}

.dialog-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

.dialog-buttons button {
  margin-left: 0;
}
</style>
