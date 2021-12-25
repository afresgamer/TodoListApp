export interface ISettingViewModel {
  NotificationSettingsFlg: boolean
  CategoryMasterFlg: boolean
}

export class SettingViewModel implements ISettingViewModel {
  NotificationSettingsFlg = false
  CategoryMasterFlg = false
}
