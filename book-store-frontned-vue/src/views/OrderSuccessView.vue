<template>
  <div class="order-success">
    <h1>Order Successful!</h1>

    <button @click="goHome">Continue Shopping</button>
  </div>
</template>

<script>
export default {

  methods: {
    goHome() {
      this.$router.push('/');
    }
  }
};
</script>

<style scoped>
.order-success{
  min-height: 350px;
}

h1 {
  color: #4CAF50;
}

button {
  padding: 10px 20px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 20px;
}
</style>