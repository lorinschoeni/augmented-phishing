import { createRouter, createWebHashHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/web',
    name: 'web',
    component: () => import('../views/WebView.vue'),
    meta: {
      show: 'web'
    }
  },
  {
    path: '/headset',
    name: 'headset',
    component: () => import('../views/WebView.vue'),
    meta: {
      show: 'headset'
    }
  },
  {
    path: '/admin',
    name: 'admin',
    component: () => import('../views/AdminView.vue')
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
