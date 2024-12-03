import type { PublicationVM } from '@/interfaces'
import Axios from '@/axios'

export default {
  async $GetExperiences() {
    return await Axios.get('api/Publication/GetExperiences')
  },
  async $GetMyExperiences(userId: number) {
    return await Axios.get(`api/Publication/GetMyExperiences?userId=${userId}`)
  },
  async $SaveExperience(publication: PublicationVM) {
    return await Axios.post('api/Publication/SaveExperience', publication)
  },
}
