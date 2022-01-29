<template>
  <div>
    <b-breadcrumb class="breadcrumb-main-area">
      <b-breadcrumb-item href="/home">
        ホーム
      </b-breadcrumb-item>
      <b-breadcrumb-item v-if="scheduleId > 0" active>
        スケジュール更新
      </b-breadcrumb-item>
      <b-breadcrumb-item v-else active>
        スケジュール作成
      </b-breadcrumb-item>
    </b-breadcrumb>
    <b-container>
      <div v-if="scheduleId === 0">
        <div class="py-5 text-center">
          <h2>新規スケジュール作成</h2>
          <p class="lead">
            スケジュールを新規で作成出来ます。
          </p>
        </div>
      </div>
      <div v-else>
        <div class="py-5 text-center">
          <h2>スケジュール更新</h2>
          <p class="lead">
            スケジュールを更新出来ます。
          </p>
        </div>
      </div>
      <validation-observer v-slot="{ invalid }">
        <b-form class="py-4">
          <b-form-group>
            <label class="float-left">スケジュール名 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="schedule.ScheduleName" rules="required|min:2|max:30" fieldname="任意のスケジュール名を入力" :disabled="scheduleId > 0" />
          </b-form-group>
          <b-form-group>
            <label class="float-left">概要内容 </label>
            <b-form-textarea
              v-model="schedule.Summary"
              placeholder="概要を入力"
              rows="3"
              max-rows="3"
              no-resize
            />
          </b-form-group>
          <b-form-group>
            <label class="float-left">開始日 <base-badge :varient-type="4" /></label>
            <b-form-datepicker
              v-model="schedule.StartDay"
              placeholder="任意の開始日を設定"
              today-button
              close-button
              locale="ja"
            />
          </b-form-group>
          <b-form-group>
            <label class="float-left">終了日 <base-badge :varient-type="4" /></label>
            <b-form-datepicker
              v-model="schedule.EndDay"
              placeholder="任意の終了日を設定"
              today-button
              close-button
              locale="ja"
            />
          </b-form-group>
          <b-form-group>
            <label class="float-left">使用時間 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="schedule.UsageTime" rules="required|min:1|max:8" fieldname="任意の使用時間を入力" />
          </b-form-group>
          <!-- <b-form-group v-show="schedule.NoticeSettingFlg">
            <label class="float-left">通知設定</label>
            <div class="text-center mr-5 pr-5">
              <b-form-checkbox v-model="schedule.LineFlg" class="mr-1">
                LINE通知
              </b-form-checkbox>
              <b-form-checkbox v-model="schedule.SlackFlg">
                Slack通知
              </b-form-checkbox>
            </div>
          </b-form-group> -->
          <!-- <div v-show="schedule.LineFlg">
            <b-form-group>
              <label>LINEアカウント </label>
              <b-form-input
                id="input-1"
                v-model="schedule.LineAccount"
                placeholder="任意のアカウントを入力"
              />
            </b-form-group>
            <b-form-group>
              <label>LINEパスワード </label>
              <b-form-input
                id="input-1"
                v-model="schedule.LinePassword"
                placeholder="任意のパスワードを入力"
              />
            </b-form-group>
          </div> -->
          <!-- <div v-show="schedule.SlackFlg">
            <b-form-group>
              <label>Slackアカウント </label>
              <b-form-input
                id="input-1"
                v-model="schedule.SlackAccount"
                placeholder="任意のアカウントを入力"
              />
            </b-form-group>
            <b-form-group>
              <label>Slackパスワード </label>
              <b-form-input
                id="input-1"
                v-model="schedule.SlackPassword"
                placeholder="任意のパスワードを入力"
              />
            </b-form-group>
          </div> -->
          <b-form-group class="mb-5">
            <label>カテゴリー </label>
            <div v-if="schedule.CategoryMasterFlg">
              <b-form-select v-model="schedule.CategoryMasterId" :options="getCategoryMasters" />
            </div>
            <div v-else>
              <b-form-select v-model="schedule.CategoryMasterId" :options="getCategoryMasters" disabled />
            </div>
          </b-form-group>
          <div class="bt-2">
            <hr>
            <b-button block variant="primary" size="lg" :disabled="invalid" @click="register(isEdit)">
              スケジュール{{ isEdit ? '更新' : '登録' }}
            </b-button>
            <b-button
              v-show="isEdit"
              block
              variant="danger"
              size="lg"
              :disabled="invalid"
              @click="deleteData"
            >
              スケジュール削除
            </b-button>
          </div>
        </b-form>
      </validation-observer>
      <footer class="my-5 pt-5 text-muted text-center text-small">
        <p class="mb-1">
          他機能一覧
        </p>
        <ul class="list-inline">
          <li class="list-inline-item">
            <a href="/schedule/list">スケジュール一覧</a>
          </li>
        </ul>
      </footer>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import _ from 'lodash'
import { CategoryMaster, ScheduleViewModel } from '../../../types'
import CommonApi from '../../../CommonApi'

interface Data {
  scheduleId: number
  schedule: ScheduleViewModel
  categoryMasters: CategoryMaster[]
}

interface CategoryMasterOption {
  value: any
  text: string
}

export default Vue.extend({
  components: {},
  data (): Data {
    return {
      scheduleId: 0,
      schedule: new ScheduleViewModel(),
      categoryMasters: []
    }
  },
  computed: {
    isEdit (): boolean {
      return this.scheduleId > 0
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
      const path = this.$route.path.split('/')
      this.scheduleId = Number(path[2])
      this.schedule = await CommonApi.ApiSchedule.GetSchedule(this.scheduleId)
      if (this.schedule.StartDay.toString() === '0001-01-01T00:00:00') { this.schedule.StartDay = new Date() }
      if (this.schedule.EndDay.toString() === '0001-01-01T00:00:00') { this.schedule.EndDay = new Date() }
      this.categoryMasters = await CommonApi.ApiCategoryMaster.GetCategoryMasterDataList()
    },
    register (isUpdate: boolean) : void {
      const meg = isUpdate ? '更新してもよろしいでしょうか？' : '登録してもよろしいでしょうか？'

      this.$bvModal.msgBoxConfirm(meg, {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (messageResult) => {
        if (!messageResult) { return }

        if (!isUpdate) {
          this.$nuxt.$loading.start()
          await CommonApi.ApiSchedule.InsertSchedule(this.schedule).then((result) => {
            this.$nuxt.$loading.finish()
            location.href = `/schedule/${result}/edit`
          }).catch()
        } else {
          this.$nuxt.$loading.start()
          const result = await CommonApi.ApiSchedule.UpdateSchedule(this.schedule)
          this.$nuxt.$loading.finish()
          if (result) { location.href = `/schedule/${this.scheduleId}/edit` }
        }
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
      }).then(async (messageResult) => {
        if (!messageResult) { return }

        await CommonApi.ApiSchedule.DeleteSchedule(this.scheduleId)
        location.href = '/home'
      })
    }
  }
})
</script>

<style>
.breadcrumb-main-area {
  margin-left: 80px;
  width: 90%;
}

.container {
  margin-top: 10px;
  max-width: 600px;
  background: lightgrey;
}
</style>
