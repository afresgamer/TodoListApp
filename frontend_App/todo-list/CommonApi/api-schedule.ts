import request from '~/plugins/request'
import { ScheduleViewModel } from '~/types'

function GetSchedule (scheduleId: number) {
  const path = `/schedule/${scheduleId}`
  return request.get<ScheduleViewModel>(path, { scheduleId })
}

function InsertSchedule (viewModel: ScheduleViewModel) {
  const path = '/schedule'
  return request.postHeader<number>(path, viewModel)
}

function UpdateSchedule (viewModel: ScheduleViewModel) {
  const path = '/schedule'
  return request.putHeader<boolean>(path, viewModel)
}

function DeleteSchedule (scheduleId: number) {
  const path = `/schedule/${scheduleId}`
  return request.delete(path, { scheduleId })
}

export default {
  GetSchedule,
  InsertSchedule,
  UpdateSchedule,
  DeleteSchedule
}
