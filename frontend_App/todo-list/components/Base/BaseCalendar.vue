<template>
  <div class="app">
    <FullCalendar :options="calendarOptions" @eventClick="handleEventClick" />
    <kantan-schedule :visible.sync="isKantanSchedule" :calendar-id="calendarId" @refresh="refresh" />
  </div>
</template>

<script>
import Vue from 'vue'
import _ from 'lodash'
import FullCalendar from '@fullcalendar/vue'
import interactionPlugin from '@fullcalendar/interaction'
import timeGridPlugin from '@fullcalendar/timegrid'
import dayGridPlugin from '@fullcalendar/daygrid'
import jaLocale from '@fullcalendar/core/locales/ja'
import { CalendarEventViewModel } from '../../types'
import CommonApi from '../../CommonApi'
import KantanSchedule from '../../components/Modal/KantanSchedule.vue'

export default Vue.extend({
  components: {
    FullCalendar,
    KantanSchedule
  },
  data () {
    return {
      isKantanSchedule: false,
      textColor: '#000',
      calendarId: 0,
      calendarOptions: {
        plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
        locale: jaLocale,
        initialView: 'dayGridMonth',
        events: [],
        // dateClick: this.handleDateClick,
        eventClick: this.handleEventClick,
        headerToolbar: {
          left: 'today btnAdd',
          center: 'title',
          right: 'prev,next dayGridMonth,timeGridWeek'
        },
        customButtons: {
          btnAdd: {
            text: 'かんたん登録',
            click: () => {
              this.calendarId = 0
              this.isKantanSchedule = true
            }
          }
        }
      }
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    getInitData () {
      this.calendarOptions.events = []
      Promise.resolve(CommonApi.ApiCalendar.GetCalendarList()).then((result) => {
        _.each(result, (data) => {
          const res = {
            id: data.Id,
            title: data.Title,
            start: data.Start,
            end: data.End,
            textColor: '#000000',
            backgroundColor: '#00FF00',
            borderColor: '#000000',
            extendedProps: {
              ScheduleId: data.ExtendedProps.ScheduleId,
              Summary: data.ExtendedProps.Summary,
              UsageTime: data.ExtendedProps.UsageTime,
              CategoryMasterFlg: data.ExtendedProps.CategoryMasterFlg,
              CategoryMasterId: data.ExtendedProps.CategoryMasterId
            }
          }

          this.calendarOptions.events.push(res)
        })
      })
    },
    // handleDateClick (arg) {
    //   alert('date click! ' + arg.dateStr)
    //   // eslint-disable-next-line no-console
    //   console.log(arg)
    // },
    handleEventClick (arg) {
      // eslint-disable-next-line no-console
      console.log(arg)
      const id = this.getCalendarIdViewModel(arg)
      this.calendarId = id
      this.isKantanSchedule = true
    },
    getCalendarIdViewModel (info) {
      return info.event.id
    },
    setEventObjToViewModel (info) {
      const res = new CalendarEventViewModel()
      res.Id = info.event.id
      res.AllDay = info.event.allDay
      res.Title = info.event.title
      res.Start = info.event.start
      // res.startHour = info.event.Start.getUTCHours().toString()
      // res.startMinute = info.event.Start.getUTCMinutes().toString()
      // res.textColor = this.textColor
      // res.backgroundColor = '#ff0000'
      // res.borderColor = '#000000'
      if (info.event.end === null) {
        res.End = res.Start
        // res.endHour = res.StartHour
        // res.endMinute = res.StartMinute
      } else {
        res.End = info.event.end
        // res.endHour = info.event.End.getUTCHours().toString()
        // res.endMinute = info.event.End.getUTCMinutes().toString()
      }
      res.ExtendedProps.ScheduleId = info.event.extendedProps.ScheduleId
      res.ExtendedProps.Summary = info.event.extendedProps.Summary
      res.ExtendedProps.UsageTime = info.event.extendedProps.UsageTime
      res.ExtendedProps.CategoryMasterFlg = info.event.extendedProps.CategoryMasterFlg
      res.ExtendedProps.CategoryMasterId = info.event.extendedProps.CategoryMasterId

      return res
    },
    refresh () {
      this.getInitData()
    }
  }
})
</script>

<style scoped>
.app {
  font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
  font-size: 14px;
  width: 100%;
  margin-top: 50px;
}
</style>
