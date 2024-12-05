<template>
  <div class="w-full h-full flex flex-col justify-between sm:flex-row">
    <div class="w-full h-full flex flex-col justify-center items-center">
      <drop-down-component
        :selectedBackground="backgroundColor"
        :selectedTextSize="textSize"
        @update-text-size="updateTextSize"
        @update-background="updateBackground"
        class="h-full justify-self-start self-start"
      ></drop-down-component>
      <p
        class="text-[#4B2142]"
        :class="[getTextSize === 'text-base' ? 'text-4xl' : getTextSize]"
      >
        Ingresa a tu cuenta
      </p>
      <div class="h-full flex flex-col mb-8 sm:mb-0">
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
          class="w-48 h-8 bg-[#DDA0DD] text-black self-center mt-5"
          :class="[getTextSize === 'text-base' ? 'text-lg' : getTextSize]"
          @click="loginUser"
          @keyup.enter="loginUser"
        >
          Ingresar
        </button>
        <p
          class="text-[#4B2142] leading-none"
          :class="[getTextSize === 'text-base' ? 'text-xl' : getTextSize]"
        >
          ¿Aún no tienes cuenta?
          <a
            class="font-bold underline hover:text-[#DDA0DD] active:text-[#8f668f]"
            @click="goRegisterForm"
            >Registrate Aquí</a
          >
        </p>
      </div>
    </div>
    <div
      class="w-full h-full flex items-center sm:flex-col"
      :class="getBgColor"
    >
      <div class="sm:mt-12">
        <p class="text-6xl font-bold">{{ platformName }}</p>
      </div>
      <div class="sm:mt-5">
        <p class="text-4xl text-[#FFFFF0]">{{ platformDescription }}</p>
      </div>
      <div
        class="w-full h-full flex items-center sm:items-end justify-center sm:mb-52"
      >
        <img
          alt="Web Page Logo"
          class="w-[200px] h-[200px] sm:w-[300px] sm:h-[300px]"
          src="../../Logo.png"
        />
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent } from 'vue'
import UserService from '../../services/UserService'
import { UserLoginVM, UserSessionVM } from '../../interfaces'
import { $ShowNotification } from '../../globals'
import DropDownComponent from '../Shared/DropDownMenu.vue'

export default defineComponent({
  name: 'LoginForm',
  components: {
    DropDownComponent,
  },
  data() {
    return {
      platformName: 'Mi Voz',
      platformDescription: 'Conecta, comparte, transforma',
      loginModel: {
        Email: '',
        Password: '',
      } as UserLoginVM,
      textSize: '',
      backgroundColor: '',
    }
  },
  mounted() {
    this.updateTextSize()
    this.updateBackground()
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
