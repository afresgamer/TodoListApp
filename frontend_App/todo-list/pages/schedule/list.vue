<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container fluid="lg">
      <h2>スケジュール一覧</h2>
      <div class="btn-area">
        <b-button class="float-left" variant="primary" href="/schedule/0/edit">
          新規登録
        </b-button>
        <b-button class="float-right" variant="danger" @click="deleteData()">
          <b-icon icon="trash-fill" /> 削除
        </b-button>
      </div>
      <b-table-simple size="lg">
        <b-thead head-variant="dark">
          <b-tr>
            <b-th>スケジュール名</b-th>
            <b-th style="max-width:250px;">
              概要説明
            </b-th>
            <b-th>期間</b-th>
            <b-th>使用時間</b-th>
            <b-th>カテゴリー名</b-th>
            <b-th>スケジュール更新</b-th>
            <b-th colspan="2">
              削除可否
            </b-th>
          </b-tr>
        </b-thead>
        <b-tbody>
          <b-tr v-for="(data, index) in scheduleList" :key="index">
            <b-th>{{ data.ScheduleName }}</b-th>
            <b-th style="max-width:250px;">
              {{ data.Summary }}
            </b-th>
            <b-td>{{ data.StartDay }}, {{ data.EndDay }}</b-td>
            <b-td>{{ data.UsageTime }}</b-td>
            <b-td>{{ data.CategoryMasterName }}</b-td>
            <b-td>
              <b-button variant="primary" @click="setLocation(data.ScheduleId)">
                スケジュール更新
              </b-button>
            </b-td>
            <b-th>
              <b-form-checkbox v-model="data.DeletedFlg" name="checkbox" class="ml-4" />
            </b-th>
          </b-tr>
        </b-tbody>
      </b-table-simple>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import _ from 'lodash'
import { ScheduleListViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  breadcrumbItems: any
  scheduleList: Array<ScheduleListViewModel>
}

export default Vue.extend({
  components: {},
  data (): Data {
    return {
      breadcrumbItems: [
        {
          text: 'ホーム',
          href: '/home'
        },
        {
          text: 'スケジュール一覧',
          active: true
        }
      ],
      scheduleList: []
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    async getInitData (): Promise<void> {
      this.scheduleList = await CommonApi.ApiScheduleList.GetScheduleList()
    },
    setLocation (id: number): void {
      location.href = `/schedule/${id}/edit`
    },
    deleteData (): void {
      const arr: ScheduleListViewModel[] = _.filter(this.scheduleList, (data: ScheduleListViewModel) => {
        return data.DeletedFlg === true
      })
      if (arr.length <= 0) {
        this.$bvModal.msgBoxOk('チェックを一つ以上つけてください。', {
          title: 'メッセージ',
          size: 'md',
          buttonSize: 'sm',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        })
        return
      }
      const idList = _.map(arr, (data: ScheduleListViewModel) => { return data.ScheduleId })
      this.$bvModal.msgBoxConfirm('削除してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (result) => {
        if (!result) { return }
        await CommonApi.ApiScheduleList.DeleteScheduleList(idList)
        this.getInitData()
      })
    }
  }
})
</script>

<style>
.btn-area {
  margin-top: 20px;
}

.table.b-table {
  margin-top: 80px;
}
</style>
