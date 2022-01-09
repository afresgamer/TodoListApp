<template>
  <div class="main-area">
    <b-container fluid="sm">
      <b-form class="form-signup">
        <div class="mb-4 login-area">
          <validation-observer v-slot="{ invalid }">
            <h1 class="h3 mb-3 text-center font-weight-normal">
              アカウント情報作成
            </h1>
            <div class="field">
              <label class="label">ユーザー名 <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="signup.userName" rules="required|min:2|max:16" fieldname="ユーザー名" />
            </div>
            <div class="field">
              <label class="label">ユーザー名カナ <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="signup.userNameKana" rules="required|min:2|max:16" fieldname="ユーザー名カナ" />
            </div>
            <div class="field">
              <label class="label">パスワード <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="signup.password" rules="required|min:8|max:16" fieldname="パスワード" type="password" />
            </div>
            <div class="field">
              <label class="label">メールアドレス <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="signup.mailAddress" rules="required|email|max:256" fieldname="メールアドレス" />
            </div>
            <div class="btn-area">
              <b-button class="sign-in btn-lg btn-block" variant="primary" :disabled="invalid" @click="register">
                登録
              </b-button>
            </div>
          </validation-observer>
        </div>
      </b-form>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { SignupViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  signup: SignupViewModel
}

export default Vue.extend({
  components: {},
  data () : Data {
    return { signup: new SignupViewModel() }
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
      }).then(async (messageResult) => {
        if (!messageResult) { return }
        this.$nuxt.$loading.start()
        const result = await CommonApi.ApiSignup.InsertSignupViewModel(this.signup)
        this.$nuxt.$loading.finish()
        if (result > 0) {
          this.$bvModal.msgBoxOk('登録完了しました。\nメールをご確認下さい。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          }).then(() => {
            this.$store.commit('setCreateFlg', true)
            this.$router.push('/login/login')
          })
        } else {
          this.$bvModal.msgBoxOk('登録失敗しました。\nシステム管理者にご連絡下さい。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          }).then(() => {
            this.$router.push('/signup')
          })
        }
      })
    }
  }
})
</script>

<style>
.main-area {
  width: 100%;
  margin-top: 40px;
  background-color: lightgrey;
}

.form-signup {
  max-width: 550px;
  padding: 15px;
  margin: auto;
}

.login-area {
  margin: 20px;
  padding: 15px;
  border: 1px solid black;
}

.sign-in {
  margin-top: 10px;
}

.field {
  margin-top: 5px;
}

.btn-area {
  margin-top: 60px;
}
</style>
