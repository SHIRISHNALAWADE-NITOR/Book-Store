import { createStore } from 'vuex';

const store = createStore({
  state: {
    user: null, // Default state for user
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    logout(state) {
      state.user = null;
    }
  },
  actions: {
    login({ commit }, user) {
      // Simulate login process and set user data
      commit('setUser', user);
      // Optionally store in localStorage/sessionStorage
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
    }
  },
  getters: {
    isAuthenticated(state) {
      return !!state.user;
    },
    currentUser(state) {
      return state.user;
    }
  }
});

export default store;
