import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import BooksView from '../views/BooksView.vue';
import AboutView from '../views/AboutView.vue';
import ContactView from '../views/ContactView.vue';
import PrivacyPolicyView from '../views/PrivacyPolicyView.vue';
import TermsOfServiceView from '../views/TermsOfServiceView.vue';
import SignupComponent from '@/views/SignupComponent.vue';
import LoginComponent from '@/views/LoginComponent.vue';
import IndividualProductView from '@/views/IndividualProductView.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'About',
    component: AboutView
  },
  {
    path: '/contact',
    name: 'Contact',
    component: ContactView
  },
  {
    path: '/privacy',
    name: 'PrivacyPolicy',
    component: PrivacyPolicyView
  },
  {
    path: '/terms',
    name: 'TermsOfService',
    component: TermsOfServiceView
  },
  {
    path: '/signup',
    name: 'SignupComponent',
    component: SignupComponent
  },
  {
    path: '/login',
    name: 'LoginComponent',
    component: LoginComponent
  },
  {
    path: '/books',
    name: 'Books',
    component: BooksView
  },
  {
    path: '/product/:id',
    name: 'ProductDetail',
    component: IndividualProductView,
    props: true 
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
