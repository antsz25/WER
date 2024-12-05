<template>
  <div class="dropdown" @keydown.esc="closeMenu">
    <!-- Dropdown Toggle Button -->
    <button
      ref="dropdownButton"
      @click="toggleMenu"
      :aria-expanded="isOpen"
      aria-haspopup="menu"
      class="dropdown-button"
    >
      <font-awesome-icon
        :icon="'universal-access'"
        class="w-8 h-8"
      ></font-awesome-icon>
    </button>

    <!-- Dropdown Menu -->
    <ul
      v-show="isOpen"
      role="menu"
      class="dropdown-menu"
      @keydown="handleKeydown"
    >
      <li v-for="item in menuItems" :key="item.id" role="menuitem">
        <a
          href="#"
          :tabindex="isOpen ? 0 : -1"
          ref="menuItems"
          @click="handleMenuItemClick(item)"
        >
          {{ item.label }}
        </a>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
export default {
  props: {
    selectedTextSize: {
      type: String,
      default: 'medium',
    },
    selectedBackground: {
      type: String,
      default: 'dark',
    },
  },
  data() {
    return {
      isOpen: false,
      menuItems: [
        { id: 1, label: 'Text large', action: 'large' },
        { id: 2, label: 'Text medium', action: 'medium' },
        { id: 3, label: 'Text small', action: 'small' },
        { id: 4, label: 'Background dark', action: 'dark' },
        { id: 5, label: 'Background clear', action: 'clear' },
      ],
      currentTextSize: this.selectedTextSize,
      currentBackground: this.selectedBackground,
    }
  },
  methods: {
    toggleMenu() {
      this.isOpen = !this.isOpen
      if (this.isOpen) {
        this.$nextTick(() => {
          this.$refs.menuItems[0]?.focus()
        })
      }
    },
    closeMenu() {
      this.isOpen = false
      this.$refs.dropdownButton?.focus()
    },
    handleKeydown(event) {
      const items = this.$refs.menuItems
      const currentIndex = items.findIndex(
        item => item === document.activeElement
      )

      if (event.key === 'ArrowDown') {
        const nextIndex = (currentIndex + 1) % items.length
        items[nextIndex]?.focus()
      } else if (event.key === 'ArrowUp') {
        const prevIndex = (currentIndex - 1 + items.length) % items.length
        items[prevIndex]?.focus()
      } else if (event.key === 'Tab') {
        this.closeMenu()
      }
    },
    handleMenuItemClick(item) {
      if (['large', 'medium', 'small'].includes(item.action)) {
        this.currentTextSize = item.action
        localStorage.setItem('textSize', this.textSizeClass)
        this.$emit('updateTextSize', this.textSizeClass)
      } else if (['dark', 'clear'].includes(item.action)) {
        this.currentBackground = item.action
        localStorage.setItem('backgroundColor', this.backgroundClass)
        this.$emit('updateBackground', this.backgroundClass)
      }
      this.closeMenu()
    },
  },
  computed: {
    textSizeClass(): string {
      switch (this.currentTextSize) {
        case 'large':
          return 'text-6xl'
        case 'small':
          return 'text-sm'
        case 'medium':
        default:
          return 'text-base'
      }
    },
    backgroundClass(): string {
      switch (this.currentBackground) {
        case 'dark':
          return 'bg-[#36454F] text-[#FFFFF0]'
        case 'clear':
          return 'bg-[#4F9211] text-black'
        default:
          return 'bg-[#36454F] text-[#FFFFF0]'
      }
    },
  },
  watch: {
    selectedTextSize(newVal) {
      this.currentTextSize = newVal
    },
    selectedBackground(newVal) {
      this.currentBackground = newVal
    },
  },
}
</script>

<style scoped>
.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 8px 8px;
  cursor: pointer;
}

.dropdown-menu {
  position: absolute;
  background: white;
  list-style: none;
  padding: 0;
  margin: 0;
  border: 1px solid #ddd;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  z-index: 1000;
}

.dropdown-menu li {
  padding: 8px 16px;
}

.dropdown-menu li a {
  text-decoration: none;
  color: black;
  display: block;
}

.dropdown-menu li a:focus {
  outline: none;
  background-color: #007bff;
  color: white;
}
</style>
