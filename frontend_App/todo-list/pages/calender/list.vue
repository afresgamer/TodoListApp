<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container fluid="lg">
      <div class="btn-area">
        <b-button variant="primary" href="/schedule/0/edit">
          新規登録
        </b-button>
      </div>
      <base-calendar />
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

interface Data {
  breadcrumbItems: any
}

export default Vue.extend({
  components: {},
  data (): Data {
    return {
      breadcrumbItems: [
        {
          text: 'ホーム',
          href: '/home'
        },
        {
          text: 'カレンダー',
          active: true
        }
      ]
    }
  }
})
</script>

<style>
.btn-area{
  margin-bottom: 10px;
}
</style>
