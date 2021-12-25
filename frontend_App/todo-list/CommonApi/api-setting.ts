import request from '~/plugins/request'
import { SettingViewModel } from '~/types'

function GetSetting () {
  const path = '/setting'
  return request.get<SettingViewModel>(path)
}

function UpsertSetting (data: SettingViewModel) {
  const path = '/setting'
  return request.post<boolean>(path, { data })
}

export default {
  GetSetting,
  UpsertSetting
}
