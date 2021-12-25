<template>
  <div>
    <b-navbar-nav>
      <b-nav-form class="ml-3">
        <b-button variant="outline-light" href="/signup" size="md" class="my-2 my-sm-0">
          サインアップ
        </b-button>
      </b-nav-form>
    </b-navbar-nav>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  name: 'NotLoginNavbar',
  created () {},
  methods: {
    getNowPath (): void {
      const path = this.$route.path.split('/')
      // eslint-disable-next-line no-console
      console.log(path[2])
    }
  }
})
</script>

<style>

</style>
