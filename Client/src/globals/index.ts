// Swal
import Swal, { type SweetAlertIcon } from 'sweetalert2'

function $CheckSession() {
  if (!localStorage.getItem('session')) {
    return false
  }
  return true
}
async function $ShowNotification(
  icon: SweetAlertIcon,
  title: string,
  text: string
) {
  Swal.fire({
    icon: icon,
    title: title,
    text: text,
  })
}

export { $CheckSession, $ShowNotification }
