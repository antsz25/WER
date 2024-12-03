import type { UserLoginVM, UserRegisterVM } from '@/interfaces'
import Axios from '@/axios'

export default {
  async $RegisterUser(registerModel: UserRegisterVM) {
    return await Axios.post('api/User/Register', registerModel)
  },
  async $Loginuser(loginModel: UserLoginVM) {
    return await Axios.post('api/User/Login', loginModel)
  },
}
