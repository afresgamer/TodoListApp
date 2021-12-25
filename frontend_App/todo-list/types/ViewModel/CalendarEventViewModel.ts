interface ICalendarExtendedProps {
  ScheduleId: number
  Summary: string // 概要説明
  UsageTime: number // 使用時間
  CategoryMasterFlg: boolean// カテゴリーマスタを表示するかどうか
  CategoryMasterId: number
}

interface ICalendarEventViewModel {
  Id: number
  AllDay: boolean
  Title: string
  Start: Date
  StartHour: string
  StartMinute: string
  End: Date
  EndHour: string
  EndMinute: string
  TextColor: string
  BackgroundColor: string
  BorderColor: string
  ExtendedProps: ICalendarExtendedProps
}

export class CalendarExtendedProps implements ICalendarExtendedProps {
  ScheduleId = 0
  Summary = ''
  UsageTime = 0
  CategoryMasterFlg = false
  CategoryMasterId = 0
}

export class CalendarEventViewModel implements ICalendarEventViewModel {
  Id = 0
  AllDay = false
  Title = ''
  Start = new Date()
  StartHour = ''
  StartMinute = ''
  End = new Date()
  EndHour = ''
  EndMinute = ''
  TextColor = ''
  BackgroundColor = ''
  BorderColor = ''
  ExtendedProps = new CalendarExtendedProps()
}
