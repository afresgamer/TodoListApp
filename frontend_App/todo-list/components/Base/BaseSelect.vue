<template>
  <div>
    <b-form-select :value="value" :options="options" />
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  components: {},
  props: {
    value: {
      type: Object,
      default: () => {}
    },
    options: {
      type: Array,
      default: () => {}
    }
  }
})
</script>
