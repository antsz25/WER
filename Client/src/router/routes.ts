import { type RouteRecordRaw } from 'vue-router'
import PublicExperiences from '@/views/UserViews/PublicExperiences.vue'
import NotFound from '@/views/NotFound.vue'
import LoginForm from '@/views/LandingPage/Login.vue'
import Register from '@/views/LandingPage/Register.vue'
import MyExperiences from '@/views/UserViews/MyExperiences.vue'
import MyWorks from '@/views/UserViews/MyWorks.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: NotFound,
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginForm,
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
  },
  {
    path: '/',
    name: 'PublicExperiences',
    component: PublicExperiences,
  },
  {
    path: '/MyExperiences',
    name: 'MyExperiences',
    component: MyExperiences,
  },
  {
    path: '/MyWorks',
    name: 'MyWorks',
    component: MyWorks,
  },
  {
    path: '/MyExperiences/:enterpriseId',
    name: 'ExperiencesByEnterprise',
    component: MyExperiences,
  },
]

export default routes
