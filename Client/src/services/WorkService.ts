import Axios from '@/axios'
import type { WorkVM } from '@/interfaces'

export default {
  async $GetWorksOfUser(userId: number) {
    return await Axios.get(`api/Work/GetWorksOfUser?userId=${userId}`)
  },
  async $SaveWork(Work: WorkVM) {
    return await Axios.post('api/Work/SaveWork', Work)
  },
  async $UpdateWork(Work: WorkVM) {
    return await Axios.post('api/Work/UpdateWork', Work)
  },
}
