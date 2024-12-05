<script lang="ts">
import { defineComponent } from 'vue'
import UserService from '../../services/UserService'
import { $ShowNotification } from '../../globals'
import { UserRegisterVM } from '../../interfaces'
import DropDownComponent from '../Shared/DropDownMenu.vue'

export default defineComponent({
  name: 'Register',
  components: {
    DropDownComponent,
  },
  data() {
    return {
      platformName: 'Mi Voz',
      platformDescription: 'Conecta, comparte, transforma',
      registerModel: {
        Name: '',
        Email: '',
        Password: '',
      } as UserRegisterVM,
      textSize: '' as string | null,
      backgroundColor: '' as string | null,
    }
  },
  mounted() {
    this.updateTextSize()
    this.updateBackground()
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
    updateTextSize() {
      this.textSize = localStorage.getItem('textSize')
    },
    updateBackground() {
      this.backgroundColor = localStorage.getItem('backgroundColor')
    },
    goLoginForm() {
      this.$router.push({ name: 'Login' })
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
  <div class="w-full h-full flex flex-col justify-between sm:flex-row">
    <drop-down-component
      class="absolute w-0 h-0"
      :class="getBgColor"
      :selectedBackground="backgroundColor"
      :selectedTextSize="textSize"
      @update-text-size="updateTextSize"
      @update-background="updateBackground"
    ></drop-down-component>
    <div
      class="w-full h-full flex flex-col-reverse justify-between sm:flex-row"
    >
      <div
        class="w-full h-full flex items-center sm:flex-col"
        :class="getBgColor"
      >
        <div class="sm:mt-12">
          <p
            :class="[getTextSize === 'text-base' ? 'text-6xl' : getTextSize]"
            class="font-bold"
          >
            {{ platformName }}
          </p>
        </div>
        <div class="sm:mt-5">
          <p :class="[getTextSize === 'text-base' ? 'text-4xl' : getTextSize]">
            {{ platformDescription }}
          </p>
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
      <div class="w-full h-full flex flex-col justify-center items-center">
        <p
          class="text-[#4B2142]"
          :class="[getTextSize === 'text-base' ? 'text-4xl' : getTextSize]"
        >
          Registrate
        </p>
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
        <p
          class="text-[#4B2142] leading-none mb-5 sm:mb-0"
          :class="[getTextSize === 'text-base' ? 'text-xl' : getTextSize]"
        >
          ¿Ya cuentas con una cuenta?
          <a
            class="font-bold underline hover:text-[#DDA0DD] active:text-[#8f668f]"
            @click="goLoginForm"
            >Da clic Aquí</a
          >
        </p>
      </div>
    </div>
  </div>
</template>
