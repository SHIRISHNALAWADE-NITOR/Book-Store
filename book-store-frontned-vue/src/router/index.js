import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import BooksView from "../views/BooksView.vue";
import AboutView from "../views/AboutView.vue";
import ContactView from "../views/ContactView.vue";
import PrivacyPolicyView from "../views/PrivacyPolicyView.vue";
import TermsOfServiceView from "../views/TermsOfServiceView.vue";
import SignupComponent from "@/views/SignupComponent.vue";
import LoginComponent from "@/views/LoginComponent.vue";
import IndividualProductView from "@/views/IndividualProductView.vue";
import CartView from "@/views/CartView.vue";
import store from "@/store";
import AdminView from "@/views/AdminView.vue";
import UserProfileVue from "@/views/UserProfileView.vue";
import OrderSummaryView from "@/views/OrderSummaryView.vue";
import OrderSuccessView from "@/views/OrderSuccessView.vue";
import OrderHistory from "@/views/OrderHistory.vue";
import ForgotPassword from "@/views/ForgotPassword.vue";
import ResetPassword from "@/views/ResetPassword.vue";

const routes = [
  { path: "/", name: "Home", component: HomeView },
  { path: "/about", name: "About", component: AboutView },
  { path: "/contact", name: "Contact", component: ContactView },
  { path: "/privacy", name: "PrivacyPolicy", component: PrivacyPolicyView },
  { path: "/terms", name: "TermsOfService", component: TermsOfServiceView },
  { path: "/signup", name: "SignupComponent", component: SignupComponent },
  { path: "/login", name: "LoginComponent", component: LoginComponent },
  {
    path: "/books",
    name: "Books",
    component: BooksView,
    props: (route) => ({ query: route.query }),
  },
  {
    path: "/product/:id",
    name: "ProductDetail",
    component: IndividualProductView,
    props: true,
  },
  { path: "/adminview", name: "AdminView", component: AdminView , meta: { requiresAuth: true, requiresAdmin: true }},
  { path: "/cart", name: "CartView", component: CartView },
  {
    path: "/profile",
    name: "UserProfile",
    component: UserProfileVue,
  },
  {
    path: "/order-summary",
    name: "OrderSummary",
    component: OrderSummaryView, // Route for the OrderSummary component
    props: (route) => ({ cartItems: route.params.cartItems }),
  },
  {
    path: "/order-success",
    name: "OrderSuccess",
    component: OrderSuccessView,
  },
  {
    path: '/order-history',
    name: 'OrderHistory',
    component: OrderHistory
  },
  {
    path: '/forgot-password',
    name: 'ForgotPassword',
    component: ForgotPassword
  },
  {
    path: '/reset-password',
    name: 'ResetPassword',
    component: ResetPassword
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  // scrollBehavior() {
  //   // Always scroll to the top of the page
  //   return { top: 0, behavior: 'smooth' };
  // },
});

// router.beforeEach((to, from, next) => {
//   const isAuthenticated = store.getters.isAuthenticated;
//   const currentUser = store.getters.currentUser;
//   const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
//   const requiresAdmin = to.matched.some((record) => record.meta.requiresAdmin);

//   // Check if the route requires authentication
//   if (requiresAuth && !isAuthenticated) {
//     next({ name: "LoginComponent" }); // Redirect to login if not authenticated
//   }
//   // Check if the route requires admin privileges
//   else if (requiresAdmin && (!isAuthenticated || !currentUser.isAdmin)) {
//     next({ name: "Home" }); // Redirect to home if not an admin
//   }
//   // Restrict admin users to admin view only
//   else if (isAuthenticated && currentUser.isAdmin && to.name !== "AdminView") {
//     next({ name: "AdminView" }); // Redirect admin users to AdminView
//   } else {
//     next(); // Allow access to the route
//   }
// });

router.beforeEach((to, from, next) => {
  const isAuthenticated = store.getters.isAuthenticated;
  const currentUser = store.getters.currentUser;
  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);
  const requiresAdmin = to.matched.some((record) => record.meta.requiresAdmin);

  // If the route requires authentication and the user is not authenticated
  if (requiresAuth && !isAuthenticated) {
    next({ name: "LoginComponent" }); // Redirect to login
  }
  // If the route requires admin privileges and the user is not an admin
  else if (requiresAdmin && (!isAuthenticated || !currentUser.isAdmin)) {
    next({ name: "Home" }); // Redirect to home
  }
  // If the user is an admin and the route is not AdminView, redirect to AdminView
  else if (isAuthenticated && currentUser.isAdmin && to.name !== "AdminView") {
    next({ name: "AdminView" }); // Redirect admin users to AdminView
  } else {
    next(); // Allow access to the route
  }
});


export default router;
