export interface IScheduleViewModel {
  ScheduleName: string // スケジュール名
  Summary: string // 概要説明
  StartDay: Date // 開始日
  EndDay: Date // 終了日
  UsageTime: number // 使用時間
  NoticeSettingFlg: boolean // 通知設定を表示するかどうか
  LineFlg: boolean // LINE設定
  LineAccount: string // LINEアカウント
  LinePassword: string // LINEパスワード
  SlackFlg: boolean // SLACK設定
  SlackAccount: string // SLACKアカウント
  SlackPassword: string // SLACKパスワード
  CategoryMasterFlg: boolean// カテゴリーマスタを表示するかどうか
  CategoryMasterId: number
}

export class ScheduleViewModel implements IScheduleViewModel {
  ScheduleName = ''
  Summary = ''
  StartDay = new Date()
  EndDay = new Date()
  UsageTime = 0
  NoticeSettingFlg = false
  LineFlg = false
  SlackFlg = false
  LineAccount = ''
  LinePassword = ''
  SlackAccount = ''
  SlackPassword = ''
  CategoryMasterFlg = false
  CategoryMasterId = 0
}
