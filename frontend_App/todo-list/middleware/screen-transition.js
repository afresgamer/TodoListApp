import api from '../CommonApi'
import Const from '~/static/const.json'

export default function ({ route }) {
  const path = route.path.split('/')
  const ignorePath = ['', null, 'error', 'login', 'signup']
  if (ignorePath.includes(path[1])) { return }

  const toPath = path[1]
  return api.ApiScreenTransition.screenTransitionAuthority(toPath).then((res) => {
    if (!res) { location.href = `/error/${Const.httpStatusCode.Forbidden}/errorPage` }
  }).catch()
}
