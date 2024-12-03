import axios, { type AxiosInstance } from 'axios'
let publicUrl: string = 'http://localhost:3001'

const token: string | null = localStorage.getItem('token')
const Axios: AxiosInstance = axios.create({
  baseURL: publicUrl,
  headers: {
    'Content-Type': 'application/json',
    Authorization: token ? `${token}` : null,
  },
})

export default Axios
