<template>
  <div>
    <b-navbar-nav>
      <b-nav-item @click="clickKino('/schedule/0/edit')">
        新規スケジュール作成
      </b-nav-item>
      <b-nav-item @click="clickKino('/schedule/list')">
        予定一覧
      </b-nav-item>
      <b-nav-item @click="clickKino('/calender/list')">
        カレンダー確認
      </b-nav-item>
      <b-nav-item @click="clickKino('/categoryMaster/list')">
        カテゴリーマスタ一覧
      </b-nav-item>
      <b-nav-item @click="clickKino('/setting/edit')">
        設定・ヘルプ
      </b-nav-item>
      <b-nav-form class="ml-3">
        <b-button variant="outline-light" size="md" class="my-2 my-sm-0" @click="logout">
          ログアウト
        </b-button>
      </b-nav-form>
    </b-navbar-nav>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import CommonApi from '../../CommonApi'

export default Vue.extend({
  name: 'LoginNavber',
  created () {},
  methods: {
    async logout () : Promise<void> {
      this.$nuxt.$loading.start()
      const result = await CommonApi.ApiLogin.Logout()
      this.$nuxt.$loading.finish()
      if (result) {
        this.$bvModal.msgBoxOk('ログアウトしました。', {
          title: 'メッセージ',
          size: 'md',
          buttonSize: 'sm',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        }).then(() => {
          this.$store.commit('setUser', 0)
          location.href = '/login/login'
        })
      }
    },
    clickKino (path: string): void {
      this.$router.push({ path })
    }
  }
})
</script>

<style>

</style>
