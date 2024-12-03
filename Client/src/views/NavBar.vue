<script lang="ts">
import { defineComponent } from 'vue'
import { $CheckSession } from '../globals'
import { UserSessionVM } from '../interfaces'
export default defineComponent({
  name: 'NavBar',
  data() {
    return {
      userModel: {} as UserSessionVM,
      moduleSelected: 0,
    }
  },
  created() {
    if (!$CheckSession()) {
      this.$router.push({ name: 'Login' })
      return
    }
    this.moduleSelected = localStorage.getItem('moduleSelected')
      ? parseInt(localStorage.getItem('moduleSelected'))
      : 0
    this.userModel = JSON.parse(localStorage.getItem('session'))
  },
  methods: {
    navigate(route: string, module: number) {
      localStorage.setItem('moduleSelected', module.toString())
      this.$router.push({ name: route })
    },
    logout() {
      if ($CheckSession) {
        localStorage.removeItem('session')
        if (localStorage.getItem('moduleSelected') !== undefined) {
          localStorage.removeItem('moduleSelected')
        }
      }
      this.$router.push('login')
    },
  },
})
</script>
<template>
  <div
    class="w-60 h-full flex flex-col space-y-5 items-center ml-5 p-2 border-black border-2 rounded-full bg-[#DCDCDC]"
  >
    <div class="w-full h-full space-y-5">
      <div class="w-52 h-52 border-black border-2 rounded-full">
        <img
          :src="userModel.Photo"
          alt="userPhoto"
          class="w-full h-full rounded-full"
        />
      </div>
      <div class="w-full h-16 text-center">
        <p class="text-2xl">{{ userModel.Name }}</p>
      </div>
      <div class="w-52 h-14 bg-black text-white text-center content-center">
        <button
          :class="{
            'bg-[#FFFFF0] text-black': moduleSelected == 1,
            'bg-black text-white': moduleSelected != 1,
          }"
          class="w-full h-full hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
          @click="navigate('PublicExperiences', 1)"
        >
          Experiencias publicas
        </button>
      </div>
      <div class="w-52 h-14 bg-black text-white text-center content-center">
        <button
          :class="{
            'bg-[#FFFFF0] text-black': moduleSelected == 2,
            'bg-black text-white': moduleSelected != 2,
          }"
          class="w-full h-full hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
          @click="navigate('MyExperiences', 2)"
        >
          Mis experiencias
        </button>
      </div>
      <div class="w-52 h-14 bg-black text-white text-center content-center">
        <button
          :class="{
            'bg-[#FFFFF0] text-black': moduleSelected == 3,
            'bg-black text-white': moduleSelected != 3,
          }"
          class="w-full h-full hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
          @click="navigate('MyWorks', 3)"
        >
          Mis empleos
        </button>
      </div>
    </div>
    <div class="flex w-full h-full items-center">
      <div class="w-52 h-14 bg-black text-white text-center content-center">
        <button
          class="w-full h-full bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
          @click="logout"
        >
          Cerrar Sesion
        </button>
      </div>
    </div>
  </div>
</template>
