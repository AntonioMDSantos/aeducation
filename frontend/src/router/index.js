import { createRouter, createWebHistory } from 'vue-router';
import StudentList from '../Students/StudentList.vue';
import StudentForm from '../Students/StudentForm.vue';
import HomePage from "../Home/HomePage.vue";

const routes = [
  {
    path: '/list',
    name: 'list',  
    component: StudentList
  },
  {
    path: '/',
    name: 'home',  
    component: HomePage
  },
  {
    path: '/create',
    name: 'create',  
    component: StudentForm
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
