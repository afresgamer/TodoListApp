export interface IScheduleListViewModel {
  ScheduleId: number
  ScheduleName: string // スケジュール名
  Summary: string // 概要説明
  StartDay: string // 開始日
  EndDay: string // 終了日
  UsageTime: string // 使用時間
  CategoryMasterId: number
  CategoryMasterName: string
  DeletedFlg: boolean
}

export class ScheduleListViewModel implements IScheduleListViewModel {
  ScheduleId = 0
  ScheduleName = ''
  Summary = ''
  StartDay = ''
  EndDay = ''
  UsageTime = ''
  CategoryMasterId = 0
  CategoryMasterName = ''
  DeletedFlg = false
}
