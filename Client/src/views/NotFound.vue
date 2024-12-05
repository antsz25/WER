<script lang="ts">
import { defineComponent } from 'vue'
import NavBar from './NavBar.vue'
export default defineComponent({
  name: 'NotFound',
  components: {
    NavBar,
  },
  data() {
    return {
      title: 'NotFound',
      textSize: '' as string | null,
      backgroundColor: '' as string | null,
    }
  },
  mounted() {
    this.updateTextSize()
    this.updateBackground()
  },
  methods: {
    updateTextSize() {
      this.textSize = localStorage.getItem('textSize')
    },
    updateBackground() {
      this.backgroundColor = localStorage.getItem('backgroundColor')
    },
  },
  computed: {
    getTextSize(): string | null {
      return this.textSize
    },
    getBgColor(): string | null {
      return this.backgroundColor
    },
  },
})
</script>
<template>
  <div class="w-full h-full flex" :class="getBgColor">
    <nav-bar
      :selected-background="backgroundColor"
      :selected-text-size="textSize"
      @update-text-size="updateTextSize"
      @update-background="updateBackground"
    ></nav-bar>
    <div
      class="flex flex-col items-center justify-center flex-grow text-center"
    >
      <h1
        class="font-bold mb-4"
        :class="[getTextSize === 'text-base' ? 'text-6xl' : getTextSize]"
      >
        404
      </h1>
      <p
        class="mb-6"
        :class="[getTextSize === 'text-base' ? 'text-2xl' : getTextSize]"
      >
        Page Not Found
      </p>
      <router-link
        to="/"
        class="px-6 py-3 bg-blue-500 hover:bg-blue-700 text-white font-semibold rounded"
      >
        Go to Homepage
      </router-link>
    </div>
  </div>
</template>
