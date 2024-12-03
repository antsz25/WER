<script lang="ts">
import { defineComponent } from 'vue'
import UserService from '../../services/UserService'
import { UserLoginVM, UserSessionVM } from '../../interfaces'
import { $ShowNotification } from '../../globals'

export default defineComponent({
  name: 'LoginForm',
  data() {
    return {
      platformName: 'Mi Voz',
      platformDescription: 'Conecta, comparte, transforma',
      loginModel: {
        Email: '',
        Password: '',
      } as UserLoginVM,
    }
  },
  methods: {
    async loginUser() {
      await UserService.$Loginuser(this.loginModel)
        .then(response => {
          const session: UserSessionVM = response.data
          localStorage.setItem('session', JSON.stringify(session))
          this.$router.push({ name: 'PublicExperiences' })
        })
        .catch(error => {
          $ShowNotification('error', 'Error', `${error.response.data}`)
        })
    },
    goRegisterForm() {
      this.$router.push({ name: 'Register' })
    },
  },
})
</script>
<template>
  <div class="w-full h-full flex flex-col justify-between sm:flex-row">
    <div class="w-full h-full flex flex-col justify-center items-center">
      <p class="text-[#4B2142] text-4xl">Ingresa a tu cuenta</p>
      <div class="flex flex-col">
        <input
          v-model="loginModel.Email"
          type="text"
          placeholder="Correo Electronico"
          class="w-80 h-10 mt-5 border-2 border-[#4B2142] rounded-md p-2 shadow-xl"
          @keyup.enter="loginUser"
        />
        <input
          v-model="loginModel.Password"
          type="password"
          placeholder="Contraseña"
          class="w-80 h-10 mt-5 border-2 border-[#4B2142] rounded-md p-2 shadow-xl"
          @keyup.enter="loginUser"
        />
        <button
          type="button"
          class="w-48 h-8 bg-[#DDA0DD] text-black text-lg self-center mt-5"
          @click="loginUser"
          @keyup.enter="loginUser"
        >
          Ingresar
        </button>
      </div>
      <p class="text-[#4B2142] text-xl leading-none">
        ¿Aún no tienes cuenta?
        <a
          class="font-bold underline hover:text-[#DDA0DD] active:text-[#8f668f]"
          @click="goRegisterForm"
          >Registrate Aquí</a
        >
      </p>
    </div>
    <div class="w-full h-full flex items-center flex-col bg-[#36454F]">
      <div class="mt-12">
        <p class="text-6xl font-bold">{{ platformName }}</p>
      </div>
      <div class="mt-5">
        <p class="text-4xl text-[#FFFFF0]">{{ platformDescription }}</p>
      </div>
      <div class="w-full h-full flex items-end justify-center mb-52">
        <img class="w-[300px] h-[300px]" src="../../Logo.png" />
      </div>
    </div>
  </div>
</template>
