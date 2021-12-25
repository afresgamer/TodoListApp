import request from '~/plugins/request'
import { CalendarEventViewModel } from '~/types'

function GetCalendar (calendarId: number) {
  const path = `/calendar/${calendarId}`
  return request.get<CalendarEventViewModel>(path, { calendarId })
}

function GetCalendarList () {
  const path = '/calendar'
  return request.get<Array<CalendarEventViewModel>>(path)
}

function InsertCalendar (viewModel: CalendarEventViewModel) {
  const path = '/calendar'
  return request.postHeader<boolean>(path, viewModel)
}

function UpdateCalendar (viewModel: CalendarEventViewModel) {
  const path = '/calendar'
  return request.putHeader<boolean>(path, viewModel)
}

function DeleteCalendar (calendarId: number) {
  const path = `/calendar/${calendarId}`
  return request.delete(path, { calendarId })
}

export default {
  GetCalendar,
  GetCalendarList,
  InsertCalendar,
  UpdateCalendar,
  DeleteCalendar
}
