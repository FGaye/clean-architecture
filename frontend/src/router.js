import { createRouter, createWebHistory } from 'vue-router';

// Import your page components
import Create from './components/Create.vue';
import Edit from './components/Edit.vue';
import Movie from './components/Movie.vue'
import SignIn from './components/SignIn.vue'
import SignUp from './components/SignUp.vue'

const routes = [
    { path: '/', component: SignUp , name: 'signup' },
    { path: '/signin', component: SignIn , name: 'signin' },
    { path: '/movies', component: Movie , name: 'movies' },
    { path: '/create', component: Create },
    { path: '/:id/edit', component: Edit },
  ];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
