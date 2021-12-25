<template>
  <div>
    <b-button v-b-modal.baseModal variant="link">
      こちら
    </b-button>
    <b-modal
      id="baseModal"
      size="lg"
      centered
      ok-only
      ok-title="閉じる"
      ok-variant="secondary"
      hide-header-close
      title="操作手順"
    >
      <b-form>
        <div class="mb-4">
          <div class="description">
            1.スケジュール作成画面でスケジュールを作成する。（通知設定もこの時に設定してください。）
          </div>
          <div class="description">
            2.スケジュール一覧やカレンダーで作成したスケジュールを確認する。
          </div>
          <div class="description">
            3.スケジュールが変更された場合、スケジュール一覧やカレンダーからスケジュール更新画面で変更する。
          </div>
          <div class="description">
            4.設定を変更したい場合は設定画面で操作設定を変更する。
          </div>
        </div>
      </b-form>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  components: {},
  props: {}
})
</script>

<style>

</style>
