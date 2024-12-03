<script lang="ts">
import { defineComponent } from 'vue'
import UserService from '../../services/UserService'
import { $ShowNotification } from '../../globals'
import { UserRegisterVM } from '../../interfaces'

export default defineComponent({
  name: 'Register',
  data() {
    return {
      platformName: 'Mi Voz',
      platformDescription: 'Conecta, comparte, transforma',
      registerModel: {
        Name: '',
        Email: '',
        Password: '',
      } as UserRegisterVM,
    }
  },
  methods: {
    async registerUser() {
      if (this.registerModel.Name === '') {
        $ShowNotification('warning', 'Warning', `Fill all the fields`)
        return
      }
      if (this.registerModel.Email === '') {
        $ShowNotification('warning', 'Warning', `Fill all the fields`)
        return
      }
      if (this.registerModel.Password === '') {
        $ShowNotification('warning', 'Warning', `Fill all the fields`)
        return
      }
      await UserService.$RegisterUser(this.registerModel)
        .then(() => {
          $ShowNotification('success', 'Success', 'User registered')
          this.$router.push({ name: 'Login' })
        })
        .catch(error => {
          $ShowNotification('error', 'Error', `${error}`)
        })
    },
    goLoginForm() {
      this.$router.push({ name: 'Login' })
    },
  },
})
</script>
<template>
  <div class="w-full h-full flex flex-col justify-between sm:flex-row">
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
    <div class="w-full h-full flex flex-col justify-center items-center">
      <p class="text-[#4B2142] text-4xl">Registrate</p>
      <div class="flex flex-col">
        <input
          v-model="registerModel.Name"
          type="text"
          placeholder="Nombre"
          class="w-80 h-10 mt-5 border-2 border-[#4B2142] rounded-md p-2 shadow-xl"
          @keyup.enter="registerUser"
        />
        <input
          v-model="registerModel.Email"
          type="email"
          placeholder="Correo electronico"
          class="w-80 h-10 mt-5 border-2 border-[#4B2142] rounded-md p-2 shadow-xl"
          @keyup.enter="registerUser"
        />
        <input
          v-model="registerModel.Password"
          type="password"
          placeholder="Contraseña"
          class="w-80 h-10 mt-5 border-2 border-[#4B2142] rounded-md p-2 shadow-xl"
          @keyup.enter="registerUser"
        />
        <button
          type="button"
          class="w-48 h-8 bg-[#DDA0DD] text-black text-lg self-center mt-5"
          @click="registerUser"
          @keyup.enter="registerUser"
        >
          Registrar
        </button>
      </div>
      <p class="text-[#4B2142] text-xl leading-none">
        ¿Ya cuentas con una cuenta?
        <a
          class="font-bold underline hover:text-[#DDA0DD] active:text-[#8f668f]"
          @click="goLoginForm"
          >Da clic Aquí</a
        >
      </p>
    </div>
  </div>
</template>
