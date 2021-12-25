<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container fluid="lg">
      <div>
        <base-badge :varient-type="5" label="テスト" />
        ・何かしてください。
      </div>
      <div>
        <b-alert show>
          Defaultのアラート
        </b-alert>
      </div>
      <div>
        <b-alert show variant="success">
          Successのアラート
        </b-alert>
        <b-alert show variant="info">
          <a href="#" class="alert-link">Info Alert</a>
        </b-alert>
      </div>
      <div>
        <b-avatar variant="primary" text="田中" />
      </div>
      <div>
        <div class="text-center my-3">
          <b-button v-b-tooltip.hover title="何か入力して下さい。">
            テスト・・・（仮）
          </b-button>

          <base-tool-tip content="私はエンジニアです。（元飲食店勤務です）" :is-tool-tip="true" class="mt-4">
            Hover Me
          </base-tool-tip>
        </div>
      </div>
      <base-card src="/Image/create-plans.jpg" name="スケジュール作成" href="#" :card-texts="cardDataList" />
      <div class="mx-auto">
        <b-button variant="outline-primary" href="#">
          OK
        </b-button>
        <b-button variant="primary" href="#">
          OK
        </b-button>
      </div>
      <div>
        <base-checkbox v-model="isCheck" description="ログイン状態を保持する（テスト）" />
      </div>
      <div>
        <base-select v-model="selected" :options="options" />
        {{ selected }}
      </div>
      <div>
        <b-table straped hover :items="items" />
      </div>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import CommonApi from '../../CommonApi'

interface CardData {
  cardText: string
  iconText: string
}

interface Data {
  TestValue: string
  placeholder: string
  items: { age: number, firstName: string, lastName: string }[]
  breadcrumbItems: any,
  cardDataList: CardData[]
  isCheck: boolean
  selected: any,
  options: any
}

export default Vue.extend({
  components: { },
  data (): Data {
    return {
      TestValue: '',
      placeholder: '入力してください。',
      items: [
        { age: 22, firstName: '田中', lastName: '信也' },
        { age: 34, firstName: '鈴木', lastName: '進' },
        { age: 64, firstName: '斎藤', lastName: '純一郎' },
        { age: 32, firstName: '中村', lastName: '健人' }
      ],
      breadcrumbItems: [
        {
          text: 'Home',
          href: '#'
        },
        {
          text: 'Test',
          href: '#'
        },
        {
          text: 'Sample',
          active: true
        }
      ],
      cardDataList: [
        {
          cardText: 'スケジュール更新',
          iconText: 'calendar-plus'
        }
      ],
      isCheck: false,
      selected: '0',
      options: [
        // { value: '0', text: 'Please select an option' },
        // { value: '1', text: 'This is First option' },
        // { value: '2', text: 'Selected Option' }
      ]
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    async getInitData (): Promise<void> {
      // this.options = (await this.$axios.get('/sample.json')).data
      this.options = (await CommonApi.ApiSample.getSample()).data
    }
  }
})
</script>
