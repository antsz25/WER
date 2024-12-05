<script lang="ts">
import { defineComponent } from 'vue'
import DropDownMenu from './views/Shared/DropDownMenu.vue'
export default defineComponent({
  name: 'App',
  components: {
    DropDownMenu,
  },
  created() {
    if (localStorage.getItem('textSize') === null) {
      localStorage.setItem('textSize', 'text-base')
    }
    if (localStorage.getItem('backgroundColor') === null) {
      localStorage.setItem('backgroundColor', 'bg-[#36454F] text-[#FFFFF0]')
    }
  },
  methods: {},
})
</script>

<template>
  <div class="w-screen h-screen">
    <router-view></router-view>
  </div>
</template>
