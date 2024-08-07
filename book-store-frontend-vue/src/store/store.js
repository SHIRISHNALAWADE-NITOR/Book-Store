import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    cartItems: []
  },
  mutations: {
    setCartItems(state, items) {
      state.cartItems = items;
    }
  },
  actions: {
    updateCartItems({ commit }, items) {
      commit('setCartItems', items);
    }
  },
  getters: {
    cartItems: state => state.cartItems
  }
});
