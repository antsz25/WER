<script lang="ts">
import { defineComponent } from 'vue'
import { $ShowNotification } from '../../globals'
import { EnterpriseVM, UserSessionVM, WorkVM } from '../../interfaces'
import EnterpriseService from '../../services/EnterpriseService'
import WorkService from '../../services/WorkService'
import NavBar from '../NavBar.vue'
export default defineComponent({
  name: 'MyWorks',
  components: {
    NavBar,
  },
  data() {
    return {
      title: 'Mis empleos',
      userModel: {} as UserSessionVM,
      workModel: {
        Id: 0,
        Enterprise: {
          Id: 0,
          Name: '',
        },
        UserId: 0,
        StartDate: '',
        EndDate: null,
      } as WorkVM,
      enterpriseList: [] as EnterpriseVM[],
      openWorkModal: false,
    }
  },
  mounted() {
    this.userModel = JSON.parse(localStorage.getItem('session'))
    this.workModel.UserId = this.userModel.Id
    this.getWorks()
  },
  methods: {
    getEnterprises() {
      EnterpriseService.$GetEnterprises()
        .then(response => {
          this.enterpriseList = response.data
        })
        .catch(error => {
          $ShowNotification('error', 'Error', `${error.data}`)
        })
    },
    getWorks() {
      WorkService.$GetWorksOfUser(this.userModel.Id)
        .then(response => {
          this.userModel.Works = response.data
        })
        .catch(error => {
          $ShowNotification('error', 'Error', error.data)
        })
    },
    showWorkModal() {
      this.openWorkModal = !this.openWorkModal
    },
    saveWork() {
      WorkService.$SaveWork(this.workModel)
        .then(() => {
          this.userModel.Works.push(this.workModel)
          this.workModel = {
            Id: 0,
            Enterprise: {
              Id: 0,
              Name: '',
            },
            UserId: this.userModel.Id,
            StartDate: '',
            EndDate: null,
          }
          $ShowNotification('success', 'Guardado', 'Empleo guardado')
        })
        .catch(error => {
          $ShowNotification('error', 'Error', error)
        })
    },
    goToEnterpriseExperiences(work: WorkVM) {
      localStorage.setItem('moduleSelected', '2')
      this.$router.push({
        name: 'ExperiencesByEnterprise',
        params: { enterpriseId: work.Enterprise.Id },
      })
    },
  },
})
</script>
<template>
  <div class="w-full h-full flex bg-[#36454F]">
    <nav-bar></nav-bar>
    <div class="flex flex-col w-full p-14">
      <p class="text-center text-6xl text-[#FFFFF0]">{{ title }}</p>
      <div class="w-full h-full overflow-y-auto bg-[#D9D9D9]">
        <table class="table-auto w-full rounded-lg border shadow-sm">
          <thead>
            <tr class="h-10">
              <th>Nombre de la empresa</th>
              <th>Rango laborado</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="work in userModel.Works" :key="work">
              <td class="min-w-96 h-10 border border-black text-center">
                {{ work.Enterprise.Name }}
              </td>
              <td class="min-w-96 h-10 border border-black">
                <div class="flex justify-around items-center">
                  <div>{{ work.StartDate }}</div>
                  <div>/</div>
                  <div>
                    {{ work.EndDate !== null ? work.EndDate : 'Still working' }}
                  </div>
                </div>
              </td>
              <td
                class="min-w-96 h-full flex justify-center border border-black"
              >
                <div class="w-52 h-10 m-2 text-center content-center">
                  <button
                    class="w-full h-full bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
                    @click="goToEnterpriseExperiences(work)"
                  >
                    Ver experiencias
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
        <div
          v-if="openWorkModal"
          class="fixed inset-0 z-50 flex bg-black bg-opacity-50 items-center justify-center"
        >
          <div class="bg-white rounded-lg p-8">
            <div class="flex w-full justify-between space-x-10">
              <h3 class="text-lg font-semibold">Agregar nuevo empleo</h3>
              <button @click="showWorkModal">
                <font-awesome-icon
                  :icon="'xmark'"
                  class="w-6 h-6"
                ></font-awesome-icon>
              </button>
            </div>
            <div class="flex flex-col w-full h-full mt-2">
              <div class="flex flex-col justify-end items-end w-full mt-4">
                <div class="flex flex-row w-full space-x-2">
                  <label
                    >Nombre de empresa
                    <input
                      type="text"
                      v-model="workModel.Enterprise.Name"
                      id="EnterpriseName"
                      class="border-2 border-black"
                    />
                  </label>
                </div>
                <div class="flex flex-row text-center w-full mt-4 space-x-2">
                  <div class="flex flex-col w-full">
                    <label for="StartDate">Fecha de inicio</label>
                    <input
                      type="date"
                      v-model="workModel.StartDate"
                      name="StartDate"
                      id="StartDate"
                      class="text-center"
                    />
                  </div>
                  <div class="flex flex-col w-full">
                    <label for="StartDate">Fecha de terminacion</label>
                    <input
                      type="date"
                      class="text-center"
                      name="EndDate"
                      id="EndDate"
                      v-model="workModel.EndDate"
                    />
                  </div>
                </div>
                <button
                  class="w-full h-full flex gap-6 mt-2 justify-center items-center bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
                  @click="saveWork(), showWorkModal()"
                >
                  <font-awesome-icon
                    :icon="'paper-plane'"
                    class="w-4 h-4"
                  ></font-awesome-icon>
                  Guardar
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="flex justify-end">
        <div class="flex flex-row w-52 h-10 m-5">
          <button
            class="w-full h-full flex gap-6 justify-center items-center bg-black text-white hover:bg-[#FFFFF0] hover:text-black active:bg-[#DCDCDCDC]"
            @click="showWorkModal"
          >
            <font-awesome-icon
              :icon="'plus'"
              class="w-6 h-6"
            ></font-awesome-icon>
            Agregar empleo
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
