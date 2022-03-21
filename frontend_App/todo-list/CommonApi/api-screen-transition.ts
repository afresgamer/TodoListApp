import request from '~/plugins/request'

function screenTransitionAuthority (toPath: string) {
  const path = '/screen-transition'
  return request.get<boolean>(path, { params: { toPath } })
}

export default { screenTransitionAuthority }
