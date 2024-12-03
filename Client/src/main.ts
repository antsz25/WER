// Imports
import { createApp } from 'vue'
import './assets/main.css'

//Router
import router from './router'

// Principal
import App from '@/App.vue'

//Icons
import { library, config } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { fas } from "@fortawesome/free-solid-svg-icons";
config.autoAddCss = false;
library.add(fas);

const app = createApp(App);
app
  .component("font-awesome-icon", FontAwesomeIcon)
  .use(router)
  .mount('#app')
