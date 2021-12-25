<template>
  <!-- 丸くするデザインは画面側で実装 -->
  <!-- <span v-if="isPill">
    <b-badge pill varient="setVarient" :href="href" @click="handleClick">{{ setLabel }}</b-badge>
  </span> -->
  <span class="badge-area">
    <b-badge :variant="setVarient" :href="href" @click="handleClick">{{ setLabel }}</b-badge>
  </span>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
  name: 'BaseBadge',
  props: {
    varientType: {
      type: Number,
      default: 0
    },
    label: {
      type: String,
      default: ''
    },
    // isPill: {
    //   type: Boolean,
    //   default: false
    // },
    href: {
      type: String,
      default: ''
    }
  },
  computed: {
    setLabel () {
      if (this.label !== '') { return this.label }
      switch (this.varientType) {
        case 4:
          return '必須'
      }
      return ''
    },
    setVarient () {
      switch (this.varientType) {
        case 1:
          return 'primary'
        case 2:
          return 'secondary'
        case 3:
          return 'success'
        case 4:
          return 'danger'
        case 5:
          return 'warning'
        case 6:
          return 'info'
        case 7:
          return 'light'
        case 8:
          return 'dark'
      }
      return ''
    }
  },
  methods: {
    handleClick (): void {
      this.$emit('click')
    }
  }
})
</script>

<style scoped>
.badge.badge-warning {
  font-size: 16px;
}
</style>
