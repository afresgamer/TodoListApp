import request from '~/plugins/request'
import { ScheduleListViewModel } from '~/types'

function GetScheduleList () {
  const path = '/schedule-list'
  return request.get<Array<ScheduleListViewModel>>(path)
}

function DeleteScheduleList (scheduleIdList: number[]) {
  const path = '/schedule-list'
  return request.deleteHeader(path, scheduleIdList)
}

export default {
  GetScheduleList,
  DeleteScheduleList
}
