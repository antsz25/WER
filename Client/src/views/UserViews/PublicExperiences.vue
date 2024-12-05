<script lang="ts">
import { defineComponent } from 'vue'
import {
  AttachmentVM,
  EnterpriseVM,
  PublicationVM,
  UserSessionVM,
} from '../../interfaces'
import NavBar from '../NavBar.vue'
import ExperienceService from '../../services/ExperiencesService'
import { $ShowNotification } from '../../globals'

export default defineComponent({
  name: 'PublicExperiences',
  components: {
    NavBar,
  },
  data() {
    return {
      title: 'Experiencias publicas',
      userModel: {} as UserSessionVM,
      publicationList: [] as PublicationVM[],
      showAttachmentsModal: false,
      selectedPublication: {} as PublicationVM,
      currentSliderIndex: 0,
      acceptedFormats: 'image/*,application/pdf',
      filterOption: '',
      searchContent: '',
      textSize: '' as string | null,
      backgroundColor: '' as string | null,
    }
  },
  mounted() {
    this.userModel = JSON.parse(localStorage.getItem('session'))
    this.getExperiences()

    this.updateTextSize()
    this.updateBackground()
  },
  methods: {
    getExperiences() {
      ExperienceService.$GetExperiences()
        .then(response => {
          this.publicationList = response.data
        })
        .catch(error => {
          $ShowNotification('error', 'Error', error)
        })
    },
    openAttachmentsModal(publication: PublicationVM) {
      this.showAttachmentsModal = !this.showAttachmentsModal
      this.selectedPublication = publication
      this.currentSliderIndex = 0
    },
    nextSlide() {
      this.currentSliderIndex =
        (this.currentSliderIndex + 1) %
        this.selectedPublication.Attachments.length
    },
    prevSlide() {
      this.currentSliderIndex =
        (this.currentSliderIndex -
          1 +
          this.selectedPublication.Attachments.length) %
        this.selectedPublication.Attachments.length
    },
    getFileTypeImage(file: AttachmentVM): boolean {
      const formats: RegExp[] = this.acceptedFormats.split(',').map(format => {
        return new RegExp(format.replace(/^[.*+?^${}()|[\]\\]/g, '\\$&'))
      })
      return formats.some(
        (regex: RegExp) =>
          regex.test(file.FileType) && file.FileType != 'application/pdf'
      )
    },
    openFile(file: AttachmentVM) {
      if (file.FileType.startsWith('image/')) {
        const byteCharacters = atob(file.Url.split(',')[1]) // Remove 'data:image/jpeg;base64,'
        const byteNumbers = new Array(byteCharacters.length)
          .fill(null)
          .map((_, i) => byteCharacters.charCodeAt(i))
        const byteArray = new Uint8Array(byteNumbers)
        const blob = new Blob([byteArray], { type: 'image/jpeg' })
        const blobUrl = URL.createObjectURL(blob)
        window.open(blobUrl, '_blank')
      } else {
        var iframe = `<iframe width='100%' height='100%' src='${file.Url}'></iframe>`
        let x: any = window.open()
        x.document.open()
        x.document.write(iframe)
        x.document.close()
      }
    },
    formatData() {
      this.publicationList.forEach(element => {
        if (element.Id !== 0) {
          if (element.Attachments !== undefined) {
            element.Attachments.forEach(attachment => {
              attachment.Url = attachment.Url + attachment.Base64
            })
          }
        }
      })
    },
    updateTextSize() {
      this.textSize = localStorage.getItem('textSize')
    },
    updateBackground() {
      this.backgroundColor = localStorage.getItem('backgroundColor')
    },
  },
  watch: {
    publicationList: {
      handler() {
        this.formatData()
      },
    },
  },
  computed: {
    filterPublicationList(): PublicationVM[] {
      switch (this.filterOption) {
        case 'mayor_calif':
          return this.publicationList
            .sort((a, b) => b.Ranking - a.Ranking)
            .filter(
              x =>
                x.Information.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                ) ||
                x.Enterprise.Name.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                )
            )
        case 'menor_calif':
          return this.publicationList
            .sort((a, b) => a.Ranking - b.Ranking)
            .filter(
              x =>
                x.Information.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                ) ||
                x.Enterprise.Name.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                )
            )
        case 'mayor_fecha':
          return this.publicationList
            .sort((a, b) => new Date(b.CreatedAt) <= new Date(a.CreatedAt))
            .filter(
              x =>
                x.Information.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                ) ||
                x.Enterprise.Name.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                )
            )
        case 'menor_fecha':
          return this.publicationList
            .sort((a, b) => new Date(a.CreatedAt) <= new Date(b.CreatedAt))
            .filter(
              x =>
                x.Information.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                ) ||
                x.Enterprise.Name.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                )
            )
        default:
          return this.publicationList
            .sort((a, b) => b.Id - a.Id)
            .filter(
              x =>
                x.Information.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                ) ||
                x.Enterprise.Name.toLowerCase().includes(
                  this.searchContent.toLowerCase()
                )
            )
      }
    },
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
    <div class="flex flex-col w-full">
      <p
        class="w-full text-center text-[#FFFFF0]"
        :class="[getTextSize === 'text-base' ? 'text-6xl' : getTextSize]"
      >
        {{ title }}
      </p>
      <div class="w-full h-16 space-x-20 flex flex-row p-5 mb-2">
        <div class="flex flex-col">
          <label for="filterComments">Filtrar comentarios</label>
          <select
            v-model="filterOption"
            name="filterComments"
            id="filterComments"
          >
            <option value="">Select an option</option>
            <option value="mayor_calif">Mayor calificacion</option>
            <option value="menor_calif">Menor calificacion</option>
            <option value="mayor_fecha">Mas recientes</option>
            <option value="menor_fecha">Mas antiguos</option>
          </select>
        </div>
        <div class="flex flex-col">
          <label for="filterByWord">Buscar</label>
          <input
            v-model="searchContent"
            type="text"
            placeholder="Buscar palabras claves"
            id="filterByWord"
            name="filterByWord"
          />
        </div>
      </div>
      <div class="w-full h-full overflow-y-auto">
        <div class="w-full flex flex-col">
          <div
            class="w-full overflow-y-auto p-5"
            v-for="experience in filterPublicationList"
            :key="experience"
          >
            <div
              class="w-full flex flex-col border border-black bg-[#DCDCDC] p-2"
            >
              <div
                class="w-full flex flex-row items-center justify-between space-x-2"
              >
                <div class="flex flex-row items-center">
                  <img
                    :src="experience.CreatedBy.Photo"
                    alt="Creator of publication photo"
                    class="w-12 h-12 rounded"
                  />
                  <span class="italic text-black text-left text-base ml-2">
                    {{ experience.CreatedBy.Name }} - {{ experience.CreatedAt }}
                  </span>
                </div>
                <div class="pr-6">
                  Experiencia en empresa:
                  {{ experience.Enterprise.Name }}
                </div>
                <div class="pr-6">
                  Calificacion: {{ experience.Ranking }} estrellas
                </div>
              </div>
              <div class="w-full p-2 h-20 overflow-y-auto">
                <p>
                  {{ experience.Information }}
                </p>
              </div>
              <div class="flex flex-row justify-between items-center w-full">
                <div class="flex">
                  <button
                    class="flex"
                    @click="openAttachmentsModal(experience)"
                  >
                    <font-awesome-icon
                      :icon="'paperclip'"
                      class="w-6 h-6"
                    ></font-awesome-icon>
                    Adjuntos
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div
          v-if="showAttachmentsModal"
          class="fixed inset-0 z-50 flex bg-black bg-opacity-50 items-center justify-center"
        >
          <div class="bg-white w-full m-48 rounded-lg p-8">
            <div class="flex w-full flex-col justify-between">
              <div class="flex w-full h-full justify-between">
                <h3
                  class="font-semibold"
                  :class="[
                    getTextSize === 'text-base' ? 'text-lg' : getTextSize,
                  ]"
                >
                  Adjuntos de publicación
                </h3>
                <button @click="openAttachmentsModal({})">
                  <font-awesome-icon
                    :icon="'xmark'"
                    class="w-6 h-6"
                  ></font-awesome-icon>
                </button>
              </div>
              <div class="flex flex-row gap-4 w-full h-96">
                <div
                  class="flex justify-start items-center hover:bg-slate-50 w-5"
                  @click="prevSlide"
                >
                  <font-awesome-icon icon="angle-left" class="w-full h-5" />
                </div>
                <div
                  class="w-full flex items-center justify-center"
                  v-if="selectedPublication.Attachments.length > 0"
                >
                  <div
                    v-for="(file, index) in selectedPublication.Attachments"
                    :key="file"
                  >
                    <img
                      :src="file.Url"
                      :alt="file.Alt"
                      class="w-full h-[350px] rounded-md inline transition-transform duration-300 transform hover:scale-125 peer"
                      @click="openFile(file)"
                      v-if="
                        index === currentSliderIndex && getFileTypeImage(file)
                      "
                    />
                    <div
                      v-else-if="index === currentSliderIndex"
                      class="w-full h-[350px] rounded-md hover: cursor-pointer flex flex-col justify-center items-center"
                      @click="openFile(file)"
                      :title="file.Alt"
                    >
                      <font-awesome-icon
                        icon="file-pdf"
                        class="w-1/2 h-1/2 m-auto inline transition-transform duration-300 transform hover:scale-125 peer"
                      >
                      </font-awesome-icon>
                      <p class="mt-1 font-bold italic truncate max-w-80">
                        {{ file.Alt }}
                      </p>
                    </div>
                  </div>
                </div>
                <div class="w-full flex items-center justify-center" v-else>
                  <p class="text-xl">Sin adjuntos para mostrar</p>
                </div>
                <div
                  class="flex justify-start items-center hover:bg-slate-50 w-5"
                  @click="nextSlide"
                >
                  <font-awesome-icon icon="angle-right" class="w-5 h-5" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
