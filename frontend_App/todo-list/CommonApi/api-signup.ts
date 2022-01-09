import request from '~/plugins/request'
import { SignupViewModel } from '~/types'

function InsertSignupViewModel (data: SignupViewModel) {
  const path = '/signup/init-create'
  return request.postHeader<number>(path, { data })
}

export default {
  InsertSignupViewModel
}
