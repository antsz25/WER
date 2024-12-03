import type { EnterpriseVM } from '@/interfaces'
import Axios from '@/axios'

export default {
  async $GetEnterprises() {
    return await Axios.get('api/Enterprise/GetEnterprises')
  },
  async $SaveEnterprise(enterprise: EnterpriseVM) {
    return await Axios.post('api/Enterprise/SaveEnterprise', enterprise)
  },
}
