<template>
  <div class="main-area">
    <b-container fluid="sm">
      <b-form class="form-signin">
        <div class="mb-4 login-area">
          <validation-observer v-slot="{ invalid }">
            <h1 class="h3 mb-3 text-center font-weight-normal">
              サインイン
            </h1>
            <div class="field">
              <label class="label">ユーザーID <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="loginInfo.userId" rules="required" fieldname="ユーザーID" />
            </div>
            <div class="field">
              <label class="label">パスワード <base-badge :varient-type="4" /></label>
              <base-text-validation v-model="loginInfo.password" rules="required|min:8|max:16" fieldname="パスワード" type="password" />
            </div>
            <div>
              <b-button class="sign-in btn-lg btn-block" variant="primary" :disabled="invalid" @click="login">
                サインイン
              </b-button>
            </div>
            <div class="password-saisettei btn-lg btn-block">
              <password-resetting />
            </div>
          </validation-observer>
        </div>
      </b-form>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import PasswordResetting from '../../components/Modal/PasswordResetting.vue'
import { LoginViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  loginInfo: LoginViewModel
}

export default Vue.extend({
  components: { PasswordResetting },
  data (): Data {
    return {
      loginInfo: new LoginViewModel()
    }
  },
  methods: {
    login () :void {
      this.$bvModal.msgBoxConfirm('ログインしてもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async () => {
        this.$nuxt.$loading.start()
        const result = await CommonApi.ApiLogin.GetLogin(this.loginInfo)
        this.$nuxt.$loading.finish()
        if (!result) {
          this.$bvModal.msgBoxOk('ログインに失敗しました。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          })
        } else {
          this.$bvModal.msgBoxOk('正常にログインしました。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          }).then(() => {
            this.$store.commit('setUser', result)
            this.$router.push('/home')
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

.form-signin {
  max-width: 500px;
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

.password-saisettei {
  margin-top: 10px;
  margin-left: 90px;
}
</style>
