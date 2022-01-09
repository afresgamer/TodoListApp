<template>
  <div>
    <b-modal
      id="kantanModal"
      :visible="visible"
      size="lg"
      centered
      hide-footer
      :title="title"
      @show="open"
      @hidden="reset"
    >
      <validation-observer v-slot="{ invalid }">
        <b-form class="py-4">
          <b-form-group>
            <b-button class="float-left" variant="primary" :disabled="invalid" @click="register">
              {{ isEdit ? '更新' : '登録' }}
            </b-button>
            <b-button
              v-show="isEdit"
              class="float-right"
              variant="danger"
              :disabled="invalid"
              @click="deleteData"
            >
              <b-icon icon="trash-fill" /> 削除
            </b-button>
          </b-form-group>
          <b-form-group>
            <label class="float-left">スケジュール名 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="calendar.Title" rules="required|min:2|max:30" fieldname="任意のスケジュール名を入力" :disabled="isEdit" />
          </b-form-group>
          <b-form-group>
            <label class="float-left">概要内容 </label>
            <b-form-textarea
              v-model="calendar.ExtendedProps.Summary"
              placeholder="概要を入力"
              rows="3"
              max-rows="3"
              no-resize
            />
          </b-form-group>
          <b-form-group>
            <label>終日 </label>
            <b-form-checkbox v-model="calendar.AllDay" />
          </b-form-group>
          <b-form-group>
            <label class="float-left">開始日 <base-badge :varient-type="4" /></label>
            <b-form-datepicker
              v-model="calendar.Start"
              placeholder="任意の開始日を設定"
              today-button
              close-button
              locale="ja"
            />
          </b-form-group>
          <b-form-group>
            <label class="float-left">終了日 <base-badge :varient-type="4" /></label>
            <b-form-datepicker
              v-model="calendar.End"
              placeholder="任意の終了日を設定"
              today-button
              close-button
              locale="ja"
            />
          </b-form-group>
          <b-form-group>
            <label class="float-left">使用時間 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="calendar.ExtendedProps.UsageTime" rules="required|min:1|max:8" fieldname="任意の使用時間を入力" />
          </b-form-group>
          <b-form-group class="mb-5">
            <label>カテゴリー </label>
            <div v-if="calendar.ExtendedProps.CategoryMasterFlg">
              <b-form-select v-model="calendar.ExtendedProps.CategoryMasterId" :options="getCategoryMasters" />
            </div>
            <div v-else>
              <b-form-select v-model="calendar.ExtendedProps.CategoryMasterId" :options="getCategoryMasters" disabled />
            </div>
          </b-form-group>
        </b-form>
      </validation-observer>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue/*, { PropType } */ from 'vue'
import _ from 'lodash'
import { CalendarEventViewModel, CategoryMaster } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  title: string
  calendar: CalendarEventViewModel
  categoryMasters: CategoryMaster[]
}

interface CategoryMasterOption {
  value: any
  text: string
}

export default Vue.extend({
  props: {
    visible: {
      type: Boolean,
      default: false
    },
    calendarId: {
      type: [Number, String],
      default: 0
    }
    // event: {
    //   type: Object as PropType<CalendarEventViewModel>,
    //   default: new CalendarEventViewModel()
    // }
  },
  data (): Data {
    return {
      title: 'スケジュール登録',
      calendar: new CalendarEventViewModel(),
      categoryMasters: []
    }
  },
  computed: {
    isEdit (): boolean {
      return Number(this.calendarId) > 0
    },
    getCategoryMasters () : CategoryMasterOption {
      const options: any = []
      options.push({
        value: 0,
        text: '--'
      })
      _.each(this.categoryMasters, (result: CategoryMaster) => {
        options.push({
          value: result.Id,
          text: result.CategoryName
        })
      })
      return options
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    async getInitData (): Promise<void> {
      this.calendar = await CommonApi.ApiCalendar.GetCalendar(Number(this.calendarId))
      this.categoryMasters = await CommonApi.ApiCategoryMaster.GetCategoryMasterDataList()
    },
    register (): void {
      const meg = this.isEdit ? '更新してもよろしいでしょうか？' : '登録してもよろしいでしょうか？'
      this.$bvModal.msgBoxConfirm(meg, {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (resMes: boolean) => {
        if (!resMes) { return }
        let message = ''
        let result = false
        if (!this.isEdit) {
          this.$nuxt.$loading.start()
          result = await CommonApi.ApiCalendar.InsertCalendar(this.calendar)
          this.$nuxt.$loading.finish()
          message = result ? '登録完了しました。' : '登録失敗しました。'
        } else {
          this.$nuxt.$loading.start()
          result = await CommonApi.ApiCalendar.UpdateCalendar(this.calendar)
          this.$nuxt.$loading.finish()
          message = result ? '登録完了しました。' : '登録失敗しました。'
        }

        this.$bvModal.msgBoxOk(message, {
          title: 'メッセージ',
          size: 'md',
          buttonSize: 'sm',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        }).then(() => {
          if (result) {
            this.close()
          }
        })
      })
    },
    deleteData (): void {
      this.$bvModal.msgBoxConfirm('削除してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (result: boolean) => {
        if (!result) { return }
        await CommonApi.ApiCalendar.DeleteCalendar(this.calendar.Id)
        this.close()
      })
    },
    reset (): void {
      this.calendar = new CalendarEventViewModel()
      this.$emit('update:visible', false)
    },
    open (): void {
      this.getInitData()
    },
    close (): void {
      this.$emit('refresh')
      this.$emit('update:visible', false)
    }
  }
})
</script>
