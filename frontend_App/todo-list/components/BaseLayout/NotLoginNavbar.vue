<template>
  <div>
    <b-navbar-nav>
      <b-nav-form class="ml-3">
        <b-button v-show="unauthorized" variant="outline-light" href="/signup" size="md" class="my-2 my-sm-0">
          サインアップ
        </b-button>
      </b-nav-form>
    </b-navbar-nav>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Const from 'static/const.json'

export default Vue.extend({
  name: 'NotLoginNavbar',
  computed: {
    isForbidden () {
      return parseInt(this.$route.params.errorCode) === Const.httpStatusCode.Forbidden
    },
    unauthorized () {
      return parseInt(this.$route.params.errorCode) === Const.httpStatusCode.Unauthorized
    }
  },
  created () {},
  methods: {}
})
</script>

<style>

</style>
