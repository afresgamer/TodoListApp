<template>
  <div>
    <b-button v-b-modal.baseModal variant="primary">
      パスワードを忘れた場合
    </b-button>
    <b-modal
      id="baseModal"
      size="lg"
      centered
      hide-footer
      :title="title"
      @show="reset"
      @hidden="reset"
    >
      <validation-observer v-slot="{ invalid }">
        <b-form>
          <div class="mb-4">
            <div class="field">
              <label class="label">ユーザーID <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="resetPassword.userId" rules="required" fieldname="ユーザーID" />
            </div>
            <div class="field">
              <label class="label">新規パスワード <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="resetPassword.password" rules="required|min:8|max:16" fieldname="新規パスワード" type="password" />
            </div>
          </div>
        </b-form>
        <div>
          <b-button class="float-right" variant="primary" :disabled="invalid" @click="register">
            登録
          </b-button>
        </div>
      </validation-observer>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { ResetPasswordViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  title: string
  resetPassword: ResetPasswordViewModel
}

export default Vue.extend({
  data (): Data {
    return {
      title: 'パスワードを忘れた方は以下の情報を入力してください。',
      resetPassword: new ResetPasswordViewModel()
    }
  },
  methods: {
    register (): void {
      this.$bvModal.msgBoxConfirm('登録してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (messageResult: boolean) => {
        if (!messageResult) { return }
        this.$nuxt.$loading.start()
        const result = await CommonApi.ApiResetPassword.ResetPassword(this.resetPassword)
        this.$nuxt.$loading.finish()
        const message = result ? 'パスワード再設定完了しました。' : 'パスワード再設定失敗しました。'
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
            this.$bvModal.hide('baseModal')
          }
        })
      })
    },
    reset (): void {
      this.resetPassword = new ResetPasswordViewModel()
    },
    close (): void {
      this.$emit('refresh')
    }
  }
})
</script>
