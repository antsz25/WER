<script lang="ts">
import { defineComponent } from 'vue';
import NavBar from './NavBar.vue'
export default defineComponent({
  name: 'NotFound',
  components: {
    NavBar,
  },
  data() {
    return {
      title: 'NotFound',
    };
  },
});
</script>
<template>
<div class="w-full h-full flex bg-[#36454F]">
    <nav-bar></nav-bar>
    <div class="flex flex-col items-center justify-center flex-grow text-center">
      <h1 class="text-6xl font-bold mb-4">404</h1>
      <p class="text-2xl mb-6">Page Not Found</p>
      <router-link to="/" class="px-6 py-3 bg-blue-500 hover:bg-blue-700 text-white font-semibold rounded">
        Go to Homepage
      </router-link>
    </div>
  </div>
</template>