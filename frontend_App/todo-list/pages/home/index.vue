<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container class="main-area">
      <h1 class="text-center my-4 mb-4">
        TODOリスト
      </h1>
      <div class="card-area">
        <div class="create-plans">
          <base-card src="/Image/create-plans.jpg" name="スケジュール作成" href="/schedule/0/edit" :card-texts="createPlansList" />
        </div>
        <div class="plans-list">
          <base-card src="/Image/plans-list.png" name="スケジュール一覧" href="/schedule/list" :card-texts="plansList" />
        </div>
        <div class="calender">
          <base-card src="/Image/calender.jpg" name="カレンダー" href="/calender/list" :card-texts="calenderList" />
        </div>
        <div class="setting">
          <base-card src="/Image/setting.jpg" name="設定" href="/setting/edit" :card-texts="settingList" />
        </div>
        <div class="blank" />
      </div>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

interface CardData {
  cardText: string
  iconText: string
}

interface Data {
  breadcrumbItems: any
  createPlansList: CardData[]
  plansList: CardData[]
  calenderList: CardData[]
  settingList: CardData[]
}

export default Vue.extend({
  components: {},
  data (): Data {
    return {
      breadcrumbItems: [
        {
          text: 'ホーム',
          active: true
        }
      ],
      createPlansList: [
        {
          cardText: 'スケジュール作成・更新',
          iconText: 'calendar-plus'
        },
        {
          cardText: 'スケジュール削除',
          iconText: 'calendar-minus'
        }
      ],
      plansList: [
        {
          cardText: 'スケジュール確認',
          iconText: 'calendar-check'
        }
      ],
      calenderList: [
        {
          cardText: 'カレンダー',
          iconText: 'calendar2-fill'
        }
      ],
      settingList: [
        {
          cardText: '設定',
          iconText: 'gear-fill'
        }
      ]
    }
  }
})
</script>

<style>
.main-area {
  background: lightgrey;
  max-width: 700px;
}

.card-area {
  margin: 0 auto;
  max-width: 540px;
}

.blank {
 height: 30px;
}
</style>
