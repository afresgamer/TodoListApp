import request from '~/plugins/request'
import { SettingViewModel } from '~/types'

function GetSetting () {
  const path = '/setting'
  return request.get<SettingViewModel>(path)
}

function UpsertSetting (viewModel: SettingViewModel) {
  const path = '/setting'
  return request.postHeader(path, viewModel)
}

export default {
  GetSetting,
  UpsertSetting
}
