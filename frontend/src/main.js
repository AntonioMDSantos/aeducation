import { createApp } from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import { loadFonts } from './plugins/webfontloader';
import axios from 'axios';
import router from './router';  

axios.defaults.baseURL = process.env.VUE_APP_BACKEND_URL;
axios.defaults.headers.post['Content-Type'] = 'application/json';
loadFonts();

createApp(App)
  .use(vuetify)
  .use(router) 
  .mount('#app');
