import request from '~/plugins/request'
import { LoginViewModel } from '~/types'

function GetUserName () {
  const path = '/login/after-get-user-name'
  return request.get<String>(path)
}

function GetLogin (data: LoginViewModel) {
  const path = '/login'
  return request.put<number>(path, { data })
}

function Logout () {
  const path = '/login/logout'
  return request.put<boolean>(path)
}

export default {
  GetUserName,
  GetLogin,
  Logout
}
