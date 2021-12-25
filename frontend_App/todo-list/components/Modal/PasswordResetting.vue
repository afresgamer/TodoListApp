<template>
  <div>
    <b-button v-b-modal.baseModal variant="primary">
      パスワードを忘れた場合
    </b-button>
    <b-modal
      id="baseModal"
      size="lg"
      centered
      ok-only
      ok-title="送信"
      :title="title"
    >
      <b-form>
        <div class="mb-4">
          <b-form-group
            id="input-group-1"
            label="ユーザーID"
            label-for="input-1"
          >
            <b-form-input
              id="input-1"
              v-model="userId"
              placeholder="ユーザーID"
            />
          </b-form-group>
          <b-form-group
            id="input-group-2"
            label="メールアドレス"
            label-for="input-2"
          >
            <b-form-input
              id="input-2"
              v-model="mailAddress"
              placeholder="メールアドレス"
            />
          </b-form-group>
        </div>
      </b-form>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

interface Data {
  title: string
  userId: number
  mailAddress: string
}

export default Vue.extend({
  data (): Data {
    return {
      title: 'パスワードを忘れた方は以下の情報を入力してください。',
      userId: 0,
      mailAddress: ''
    }
  }
})
</script>
