<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container class="main-area">
      <h3>操作手順</h3>
      <b-alert show class="details">
        操作手順の詳細については<operation-details />の説明をご覧ください。
      </b-alert>
      <div class="operation-settings">
        <h3>操作設定</h3>
        <h6>通知設定</h6>
        <div class="form-check">
          <b-form-group v-slot="{ ariaDescribedby }">
            <b-form-radio-group
              v-model="NotificationSettingsValue"
              :options="NotificationSettingOptions"
              :aria-describedby="ariaDescribedby"
              name="notification-Setting"
              stacked
            />
          </b-form-group>
        </div>
        <h6>カテゴリーマスタ操作権限設定</h6>
        <div class="form-check">
          <b-form-group>
            <b-form-radio v-model="CategoryMasterValue" name="authority" value="false">
              権限なし
            </b-form-radio>
            <b-form-radio v-model="CategoryMasterValue" name="authority" value="true">
              権限あり
            </b-form-radio>
          </b-form-group>
        </div>
        <div class="register">
          <b-button class="register-btn" variant="primary" @click="register">
            登録
          </b-button>
        </div>
      </div>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import OperationDetails from '../../components/Modal/OperationDetails.vue'
import { SettingViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  breadcrumbItems: any
  NotificationSettingOptions: any
  setting: SettingViewModel
}

export default Vue.extend({
  components: { OperationDetails },
  data (): Data {
    return {
      breadcrumbItems: [
        {
          text: 'ホーム',
          href: '/home'
        },
        {
          text: '設定・ヘルプ',
          active: true
        }
      ],
      NotificationSettingOptions: [
        { text: '通知なし', value: 'false' },
        { text: '通知あり', value: 'true' }
      ],
      setting: new SettingViewModel()
    }
  },
  computed: {
    NotificationSettingsValue: {
      get () {
        const data: SettingViewModel = this.setting
        return String(data.NotificationSettingsFlg === true)
      },
      set (values) {
        this.setting.NotificationSettingsFlg = Boolean(values)
      }
    },
    CategoryMasterValue: {
      get () {
        const data: SettingViewModel = this.setting
        return String(data.CategoryMasterFlg === true)
      },
      set (values) {
        this.setting.CategoryMasterFlg = Boolean(values)
      }
    }
  },
  created () {
    this.getInitData()
  },
  mounted () {
    // eslint-disable-next-line no-console
    console.log(this.setting)
  },
  methods: {
    async getInitData (): Promise<void> {
      this.setting = await CommonApi.ApiSetting.GetSetting()
    },
    register (): void {
      this.$bvModal.msgBoxConfirm('登録してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async () => {
        this.$nuxt.$loading.start()
        const result = await CommonApi.ApiSetting.UpsertSetting(this.setting)
        this.$nuxt.$loading.finish()
        const message = result ? '登録完了しました。' : '登録失敗しました。'
        this.$bvModal.msgBoxOk(message, {
          title: 'メッセージ',
          size: 'md',
          buttonSize: 'sm',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        }).then(() => {
          this.getInitData()
        })
      })
    }
  }
})
</script>

<style>
.main-area {
  margin-top: 10px;
  width: 90%;
  background: lightgrey;
}

.register {
  background: lightgrey;
  display: block;
}
.register .register-btn {
  margin-left: 950px;
  margin-bottom: 10px;
}

.details div {
  white-space: nowrap;
  display: inline-block;
}
.details div .btn.btn-link {
  padding: 0;
  margin-bottom: 5px;
}
.btn:focus, .btn.focus {
  box-shadow: 0 0 0 0;
}
</style>
