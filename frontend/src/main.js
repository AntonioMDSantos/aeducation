import { createApp } from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import { loadFonts } from './plugins/webfontloader';
import axios from 'axios';
import router from './router';  
import ToastPlugin from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-bootstrap.css';
axios.defaults.baseURL = process.env.VUE_APP_BACKEND_URL;
axios.defaults.headers.post['Content-Type'] = 'application/json';
loadFonts();

createApp(App)
  .use(ToastPlugin)
  .use(vuetify)
  .use(router) 
  .mount('#app');
