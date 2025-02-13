import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Notifications from '@kyvg/vue3-notification'
import Vue3DraggableResizable from 'vue3-draggable-resizable'

import 'vue3-draggable-resizable/dist/Vue3DraggableResizable.css'
import '@/assets/scss/admin.scss'

createApp(App)
	.use(router)
	.use(Notifications)
	.use(Vue3DraggableResizable)
	.mount('#app')
