import request from '~/plugins/request'
import { LoginViewModel } from '~/types'

function GetLogin (data: LoginViewModel) {
  const path = '/login'
  return request.put<number>(path, { data })
}

function Logout () {
  const path = '/login/logout'
  return request.put<boolean>(path)
}

export default {
  GetLogin,
  Logout
}
