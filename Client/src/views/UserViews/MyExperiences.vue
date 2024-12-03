<script lang="ts">
import { defineComponent } from 'vue'
import {
  AttachmentVM,
  CommentVM,
  EnterpriseVM,
  LightUserVM,
  UserSessionVM,
} from '../../interfaces'
import NavBar from '../NavBar.vue'
import { PublicationVM } from '../../interfaces/index'
import ExperienceService from '../../services/ExperiencesService'
import { $ShowNotification } from '../../globals'
import EnterprisesService from '../../services/EnterpriseService'

export default defineComponent({
  name: 'MyExperiences',
  components: {
    NavBar,
  },
  data() {
    return {
      title: 'Mis experiencias',
      userModel: {
        Id: 0,
        Email: '',
        Name: '',
        Photo: '',
      } as UserSessionVM,
      experienceList: [] as PublicationVM[],
      createExperienceModal: false,
      showNewAttachmentsModal: false,
      newExperience: {} as PublicationVM,
      enterprisesList: [] as EnterpriseVM[],
      isDragging: false,
      filesPaths: [] as AttachmentVM[],
      selectedPublication: {} as PublicationVM,
      files: [] as AttachmentVM[],
      acceptedFormats: 'image/*,application/pdf',
      showAttachmentsModal: false,
      currentSliderIndex: 0,
      searchContent: '',
      filterOption: '',
      enterpriseId: undefined,
    }
  },
  mounted() {
    this.userModel = JSON.parse(localStorage.getItem('session'))
    this.getMyExperiences()
    this.getEnterprises()
    if (this.$route.params.enterpriseId !== undefined) {
      this.enterpriseId = parseInt(this.$route.params.enterpriseId)
    }
  },
  methods: {
    getMyExperiences() {
      ExperienceService.$GetMyExperiences(this.userModel.Id)
        .then(response => {
          this.experienceList = response.data
          if (this.enterpriseId !== undefined) {
            this.experienceList = this.experienceList.filter(
              x => x.Enterprise.Id === this.enterpriseId
            )
          }
        })
        .catch(error => {
          $ShowNotification('error', 'Error', error)
        })
    },
    openExperienceModal() {
      this.createExperienceModal = !this.createExperienceModal
    },
    openAttachmentsModal(publication: PublicationVM | null) {
      this.showAttachmentsModal = !this.showAttachmentsModal
      this.selectedPublication = publication
      this.currentSliderIndex = 0
    },
    openNewAttachmentsModal() {
      this.showNewAttachmentsModal = !this.showNewAttachmentsModal
      this.currentSliderIndex = 0
    },
    saveExperience() {
      this.newExperience.CreatedBy = {
        Id: this.userModel.Id,
        Name: this.userModel.Name,
        Photo: this.userModel.Photo,
      } as LightUserVM
      this.newExperience.Enterprise = this.enterprisesList.filter(
        x => x.Id === this.newExperience.Enterprise
      )[0]
      this.newExperience.Attachments = JSON.parse(JSON.stringify(this.files))
      const date = new Date()
      this.newExperience.CreatedAt = `${date.getDate()}\\${date.getMonth()}\\${date.getFullYear()}`
      this.newExperience.Comments = [] as CommentVM[]
      const publication: PublicationVM = JSON.parse(
        JSON.stringify(this.newExperience)
      )
      ExperienceService.$SaveExperience(publication)
        .then(() => {
          $ShowNotification('success', 'Guardado', 'Experiencia guardada')
          this.newExperience.CreatedBy = {} as LightUserVM
          this.newExperience.Attachments = [] as AttachmentVM[]
          this.newExperience.CreatedAt = ''
          this.newExperience.Information = ''
          this.newExperience.Ranking = 0
          this.newExperience.Enterprise = {} as EnterpriseVM
          this.newExperience.Comments = [] as CommentVM[]
          this.experienceList.push(publication)
        })
        .catch(error => {
          $ShowNotification('error', 'Error', error)
        })
    },
    getEnterprises() {
      EnterprisesService.$GetEnterprises()
        .then(response => {
          this.enterprisesList = response.data
        })
        .catch((error: any) => {
          $ShowNotification('error', 'Error', error)
        })
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
    onClick(evt: any) {
      evt.target.value = null
    },
    async onChange(evt: any) {
      const formats: RegExp[] = this.acceptedFormats.split(',').map(format => {
        return new RegExp(format.replace(/^[.*+?^${}()|[\]\\]/g, '\\$&'))
      })
      let files: File[] = [...evt.target.files].filter((y: File) =>
        formats.some((regex: RegExp) => regex.test(y.type))
      )
      for (let index = 0; index < files.length; index++) {
        const base64: string = await this.getBase64(files[index]).then(
          response => {
            return response
          }
        )
        const file = {
          Id: 0,
          PublicationId: 0,
          Url: URL.createObjectURL(files[index]),
          Base64: base64,
          UploadedBy: 0,
          FileType: files[index].type,
          Alt: files[index].name,
        } as AttachmentVM
        this.files.push(file)
        this.filesPaths.push(file)
      }
    },
    openImage(path: string) {
      var iframe = `<iframe width='100%' height='100%' src='${path}'></iframe>`
      let x: any = window.open()
      x.document.open()
      x.document.write(iframe)
      x.document.close()
    },
    openFile(file: AttachmentVM) {
      if (file.FileType.startsWith('image/')) {
        if (file.Id === 0) {
          window.open(file.Url)
          return
        }
        const byteCharacters = atob(file.Url.split(',')[1])
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
    removeAttachment(path: string) {
      const index = this.filesPaths.findIndex(x => x.Url == path)
      this.filesPaths.splice(index, 1)
      ;(this.files as AttachmentVM[]).splice(index, 1)
    },
    dragover(evt: any) {
      evt.preventDefault()
      this.isDragging = true
    },
    dragleave() {
      this.isDragging = false
    },
    drop(e: any) {
      e.preventDefault()
      this.onDrop(e.dataTransfer.files)
      this.isDragging = false
    },
    async onDrop(files: any) {
      const formats: RegExp[] = this.acceptedFormats.split(',').map(format => {
        return new RegExp(format.replace(/^[.*+?^${}()|[\]\\]/g, '\\$&'))
      })
      let correctedFiles: File[] = [...files].filter((y: File) =>
        formats.some((regex: RegExp) => regex.test(y.type))
      )
      for (let index = 0; index < correctedFiles.length; index++) {
        const base64: string = await this.getBase64(correctedFiles[index]).then(
          response => {
            return response
          }
        )
        const file = {
          Id: 0,
          PublicationId: 0,
          Url: URL.createObjectURL(files[index]),
          Base64: base64,
          UploadedBy: 0,
          FileType: files[index].type,
          Alt: files[index].name,
        } as AttachmentVM
        if (this.checkForTypeOfArray) {
          ;(this.files as AttachmentVM[]).push(file)
        } else {
          ;(this.files as AttachmentVM) = file
        }
        this.filesPaths.push(file)
      }
    },
    getBase64(_file: File): Promise<string> {
      return new Promise(resolve => {
        setTimeout(() => {
          const file = _file
          const fileReader = new FileReader()
          fileReader.onloadend = async () => {
            const testResult = await fileReader.result?.toString()
            resolve(testResult || '')
          }
          fileReader.readAsDataURL(file)
        }, 100)
      })
    },
    triggerOnClick() {
      let inputFile = document.getElementById('fileInput')
      if (inputFile) {
        inputFile.click()
      }
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
    formatData() {
      this.experienceList.forEach(element => {
        if (element.Id !== 0) {
          if (element.Attachments !== undefined) {
            element.Attachments.forEach(attachment => {
              attachment.Url = attachment.Url + attachment.Base64
            })
          }
        }
      })
    },
  },
  watch: {
    experienceList: {
      handler() {
        this.formatData()
      },
      once: true,
    },
    // enterpriseId: {
    //   handler() {
    //     this.getExperiences()
    //   },
    // },
  },
  computed: {
    filterPublicationList(): PublicationVM[] {
      switch (this.filterOption) {
        case 'mayor_calif':
          return this.experienceList
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
          return this.experienceList
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
          return this.experienceList
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
          return this.experienceList
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
          return this.experienceList
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
  },
})
</script>
<template>
  <div class="w-full h-full flex bg-[#36454F]">
    <nav-bar></nav-bar>
    <div class="flex flex-col w-full">
      <p class="text-center text-6xl text-[#FFFFF0]">{{ title }}</p>
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
            class="w-full overflow-y-auto p-6"
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
                    alt="userPhoto"
                    class="w-12 h-12 rounded"
                  />
                  <span class="ml-2 italic text-black text-left text-base">
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
      </div>
      <div
        v-if="createExperienceModal"
        class="fixed inset-0 z-50 flex bg-black bg-opacity-50 items-center justify-center"
      >
        <div class="bg-white rounded-lg p-8">
          <div class="flex w-full justify-between space-x-10">
            <h3 class="text-lg font-semibold">Agregar Nueva experiencia</h3>
            <button @click="openExperienceModal">
              <font-awesome-icon
                :icon="'xmark'"
                class="w-6 h-6"
              ></font-awesome-icon>
            </button>
          </div>
          <div class="flex flex-col w-full">
            <textarea
              class="border border-black w-96 h-32 p-2 resize-none"
              v-model="newExperience.Information"
            ></textarea>
            <div class="flex flex-row justify-between items-center w-full mt-2">
              <div class="flex">
                <button class="flex" @click="openNewAttachmentsModal">
                  <font-awesome-icon
                    :icon="'paperclip'"
                    class="w-6 h-6"
                  ></font-awesome-icon>
                  Adjuntos
                </button>
              </div>
              <div class="flex space-x-2">
                <label class="text-xl" for="calification">Calificacion</label>
                <select
                  v-model="newExperience.Ranking"
                  name="calification"
                  class="bg-black text-white"
                  id="calification"
                >
                  <option value="0">Select an option</option>
                  <option value="1">1 Estrella</option>
                  <option value="2">2 Estrellas</option>
                  <option value="3">3 Estrellas</option>
                  <option value="4">4 Estrellas</option>
                  <option value="5">5 Estrellas</option>
                </select>
              </div>
            </div>
            <div class="flex space-x-2 mt-6">
              <label class="text-xl" for="enterprises">Empresa</label>
              <select
                v-model="newExperience.Enterprise"
                name="enterprises"
                class="bg-black text-white"
                id="enterprises"
              >
                <option value="">Select an option</option>
                <option
                  v-for="enterprise in enterprisesList"
                  :key="enterprise"
                  v-bind:value="enterprise.Id"
                >
                  {{ enterprise.Name }}
                </option>
              </select>
            </div>
            <div class="flex flex-row justify-end items-end w-full h-10 mt-4">
              <button
                class="w-full h-full flex gap-6 justify-center items-center bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
                @click="saveExperience(), openExperienceModal()"
              >
                <font-awesome-icon
                  :icon="'paper-plane'"
                  class="w-4 h-4"
                ></font-awesome-icon>
                Enviar
              </button>
            </div>
          </div>
        </div>
      </div>
      <div
        v-if="showNewAttachmentsModal"
        class="fixed inset-0 z-50 flex bg-black bg-opacity-50 items-center justify-center"
      >
        <div class="bg-white w-full rounded-lg p-8 m-48">
          <div class="flex w-full justify-between space-x-10">
            <h3 class="text-lg font-semibold">Agregar Nuevos adjuntos</h3>
            <button @click="openNewAttachmentsModal">
              <font-awesome-icon
                :icon="'xmark'"
                class="w-6 h-6"
              ></font-awesome-icon>
            </button>
          </div>
          <div class="flex flex-col w-full h-full">
            <div
              id="dropContainer"
              class="flex justify-center align-middle items-center h-24 w-full rounded-md border border-dashed cursor-pointer shadow-sm"
              @dragover="dragover"
              @dragleave="dragleave"
              @drop="drop"
              @click="triggerOnClick"
            >
              <span v-if="isDragging">Release to drop files here...</span>
              <span v-else>Drop your files here or open file browser </span>
            </div>
            <input
              id="fileInput"
              class="hidden"
              type="file"
              multiple
              @change="onChange"
              @click="onClick"
              accept="image/*,application/pdf"
            />
            <div class="flex flex-row flex-wrap gap-6 mt-2">
              <div
                v-for="file in filesPaths"
                :key="file"
                class="min-w-[160px] min-h-[160px] max-w-[160px] max-h-[160px] hover:opacity-85"
              >
                <img
                  :src="file.Url"
                  :alt="file.Alt"
                  class="cursor-pointer min-h-[160px] min-w-[160px] max-w-[160px] max-h-[160px]"
                  @click="openFile(file)"
                  v-if="getFileTypeImage(file)"
                />
                <div
                  v-else
                  class="w-[160px] h-[160px] hover: cursor-pointer flex flex-col justify-center items-center overflow-y-visible"
                  @click="openFile(file)"
                  :title="file.Alt"
                >
                  <font-awesome-icon icon="file-pdf" class="w-full h-full">
                  </font-awesome-icon>
                  <p class="mt-1 font-bold w-full italic truncate">
                    {{ file.Alt }}
                  </p>
                </div>
                <button
                  type="button"
                  class="text-gray-400 bg-transparent hover:text-gray-900 text-sm inline-flex items-center"
                  @click="removeAttachment(file.Url)"
                >
                  <font-awesome-icon
                    icon="xmark"
                    class="min-w-3 min-h-3"
                  ></font-awesome-icon>
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
              <h3 class="text-lg font-semibold">Adjuntos de publicación</h3>
              <button @click="openAttachmentsModal(null)">
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
                v-if="selectedPublication.Attachments"
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
      <div class="flex justify-end">
        <div class="flex flex-row w-52 h-10 m-5">
          <button
            class="w-full h-full flex gap-6 justify-center items-center bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
            @click="openExperienceModal"
          >
            <font-awesome-icon
              :icon="'plus'"
              class="w-6 h-6"
            ></font-awesome-icon>
            Agregar experiencia
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
