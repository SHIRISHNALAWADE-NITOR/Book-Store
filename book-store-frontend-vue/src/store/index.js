// import { createStore } from 'vuex';

// const store = createStore({
//   state: {
//     user: null, // Default state for user
//   },
//   mutations: {
//     setUser(state, user) {
//       state.user = user;
//     },
//     logout(state) {
//       state.user = null;
//     }
//   },
//   actions: {
//     login({ commit }, user) {
//       // Simulate login process and set user data
//       commit('setUser', user);
//       // Optionally store in localStorage/sessionStorage
//       localStorage.setItem('user', JSON.stringify(user));
//     },
//     logout({ commit }) {
//       commit('logout');
//       localStorage.removeItem('user');
//     },
//     initialize({ commit }) {
//       const user = JSON.parse(localStorage.getItem('user'));
//       if (user) {
//         commit('setUser', user);
//       }
//     }
//   },
//   getters: {
//     isAuthenticated(state) {
//       return !!state.user;
//     },
//     currentUser(state) {
//       return state.user;
//     }
//   }
// });

// export default store;

import { createStore } from 'vuex';

const store = createStore({
  state: {
    user: null,
    cart: []
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    logout(state) {
      state.user = null;
    },
    addToCart(state, book) {
      // Avoid duplication in cart
      const existingItem = state.cart.find(item => item.bookId === book.bookId);
      if (!existingItem) {
        state.cart.push(book);
      }
    },
    removeFromCart(state, bookId) {
      state.cart = state.cart.filter(item => item.bookId !== bookId);
    },
    clearCart(state) {
      state.cart = [];
    }
  },
  actions: {
    login({ commit }, user) {
      commit('setUser', user);
      localStorage.setItem('user', JSON.stringify(user));
    },
    logout({ commit }) {
      commit('logout');
      localStorage.removeItem('user');
    },
    initialize({ commit }) {
      const user = JSON.parse(localStorage.getItem('user'));
      if (user) {
        commit('setUser', user);
      }
    },
    addToCart({ commit }, book) {
      commit('addToCart', book);
    },
    removeFromCart({ commit }, bookId) {
      commit('removeFromCart', bookId);
    },
    clearCart({ commit }) {
      commit('clearCart');
    }
  },
  getters: {
    isAuthenticated(state) {
      return !!state.user;
    },
    currentUser(state) {
      return state.user;
    },
    cart(state) {
      return state.cart;
    },
    cartItemCount(state) {
      return state.cart.length;
    },
    cartTotal(state) {
      return state.cart.reduce((total, item) => total + item.price, 0);
    }
  }
});

export default store;

