<template>
    <div>
      <h1>WELCOME {{ userName }}</h1>
  
      <div class="user-profile" v-if="user">
        <h1>User Profile</h1>
        <p><strong>Name:</strong> {{ user.name }}</p>
        <p><strong>Email:</strong> {{ user.email }}</p>
        <p><strong>Username:</strong> {{ user.username }}</p>
        <p><strong>Phone Number:</strong> {{ user.phoneNumber }}</p>
        <p><strong>Date of Birth:</strong> {{ formattedDateOfBirth }}</p>
  
        <!-- Address Section -->
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
          <div v-else>
            <p>No addresses found.</p>
            <button @click="openAddDialog">Add Address</button>
          </div>
        </div>
      </div>
  
      <div v-else>
        <p>Loading...</p>
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
              <input
                type="text"
                v-model="addressData.postalCode"
                required
                pattern="[1-9][0-9]{5}"
              />
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
            const response = await fetch(`https://localhost:7044/api/Users/${userId}`);
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
            const response = await fetch(`https://localhost:7044/api/Address/${userId}`);
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
            response = await fetch(`https://localhost:7044/api/Address/${this.editingAddress.addressId}`, {
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
            response = await fetch(`https://localhost:7044/api/Address`, {
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
  /* Style for user profile container */
  .user-profile {
    max-width: 700px;
    margin: 20px auto;
    padding: 20px;
    border: 1px solid #e0e0e0;
    border-radius: 10px;
    background-color: #ffffff;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    font-family: Arial, sans-serif;
  }
  
  /* Main header styling */
  .user-profile h1 {
    font-size: 32px;
    margin-bottom: 20px;
    color: #333333;
    text-align: center;
    font-weight: 700;
  }
  
  /* Paragraph styling */
  .user-profile p {
    font-size: 18px;
    margin: 10px 0;
    line-height: 1.6;
    text-align: left;
  }
  
  /* Strong text styling */
  .user-profile strong {
    font-weight: 700;
    color: #444444;
  }
  
  /* Address section styling */
  .address-section {
    margin-top: 30px;
  }
  
  /* Address card styling */
  .address-card {
    border: 1px solid #d0d0d0;
    padding: 15px;
    margin-bottom: 15px;
    border-radius: 8px;
    background-color: #fafafa;
    box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
  }
  
  /* Button styling */
  .address-card button,
  .dialog-content button {
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    background-color: #007bff;
    color: #ffffff;
    cursor: pointer;
  }
  
  .address-card button:hover,
  .dialog-content button:hover {
    background-color: #0056b3;
  }
  
  /* Dialog overlay styling */
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
  
  /* Dialog content styling */
  .dialog-content {
    background-color: #ffffff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    width: 90%;
    max-width: 500px;
  }
  
  .dialog-content h3 {
    margin-top: 0;
    margin-bottom: 15px;
  }
  
  .dialog-content form {
    display: flex;
    flex-direction: column;
  }
  
  .dialog-content div {
    margin-bottom: 15px;
  }
  
  .dialog-content label {
    display: block;
    margin-bottom: 5px;
  }
  
  .dialog-content input {
    width: 100%;
    padding: 10px;
    border: 1px solid #d0d0d0;
    border-radius: 4px;
  }
  
  .dialog-content small {
    color: #dc3545;
  }
  </style>
  