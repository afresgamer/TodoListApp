<template>
  <div>
    <b-navbar toggleable="lg" type="dark" variant="success">
      <b-navbar-brand>TODOリスト</b-navbar-brand>
      <b-collapse id="nav-text-collapse" is-nav>
        <b-navbar-nav class="ml-auto">
          <login-navbar v-if="isLogin" />
          <not-login-navbar v-else />
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  name: 'CommonNavber',
  components: { },
  computed: {
    // eslint-disable-next-line vue/no-dupe-keys
    isLogin () {
      return this.$store.state.UserId > 0
    },
    userId () {
      return this.$store.state.UserId
    }
  },
  watch: {
    userId () {
      this.$nextTick(() => {
        // eslint-disable-next-line no-console
        console.log('変更されました')
      })
    }
  },
  // mounted () {
  //   this.$store.subscribe((mutation, state) => {
  //     if (mutation.type === 'setUser') {
  //       // eslint-disable-next-line no-console
  //       console.log('update UserId! %s', state.UserId)
  //       this.GetNowLogin()
  //     }
  //   })
  // },
  created () {},
  methods: {}
})
</script>
