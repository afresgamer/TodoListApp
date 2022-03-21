<template>
  <div class="error-area">
    <div class="error-context">
      <div class="error-message text-danger">
        {{ errorMessage.title }}
      </div>
      <div v-show="errorMessage.message !== ''" class="error-message text-danger">
        {{ errorMessage.message }}
      </div>
    </div>
    <div v-if="!isForbidden" class="error-btn-area text-center">
      <b-button variant="primary" @click="$router.push('/home')">
        HOMEに戻る
      </b-button>
    </div>
    <div v-if="isForbidden" class="error-btn-area text-center">
      <b-button variant="primary" @click="$router.push('/login/login')">
        ログイン画面に戻る
      </b-button>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Const from 'static/const.json'

export default Vue.extend({
  components: {},
  data () {
    return {
      errorMessage: {
        title: 'エラーが発生しました。',
        message: 'システム管理者に確認してください。'
      }
    }
  },
  computed: {
    isForbidden () {
      return parseInt(this.$route.params.errorCode) === Const.httpStatusCode.Forbidden
    },
    unauthorized () {
      return parseInt(this.$route.params.errorCode) === Const.httpStatusCode.Unauthorized
    }
  },
  mounted () {
    if (this.isForbidden || this.unauthorized) {
      this.errorMessage.title = 'ページの使用権限がありません。'
      this.errorMessage.message = ''
    }
  }
})
</script>

<style>
.error-area {
  padding: 50px 250px 0 250px;
}

.error-context {
  margin-left: 1%;
  margin-right: 1%;
  border: 1px solid #ccc;
  border-collapse: separate;
  border-radius: 10px;
  border-spacing: 0;
}

.error-message {
  margin: 20px;
  text-align: center;
}

.error-btn-area {
  margin-top: 20px;
}
</style>
