import request from '~/plugins/request'
import { ResetPasswordViewModel } from '~/types'

function ResetPassword (data: ResetPasswordViewModel) {
  const path = '/reset-password'
  return request.putHeader<Boolean>(path, data)
}

export default {
  ResetPassword
}
