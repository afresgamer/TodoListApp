<template>
  <div v-if="isToolTip">
    <b-button id="target-tip">
      <slot />
    </b-button>
    <b-tooltip target="target-tip" triggers="hover">
      {{ content }}
    </b-tooltip>
  </div>
  <div v-else>
    <span>{{ value }}</span>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
export default Vue.extend({
  name: 'BaseToolTip',
  components: {},
  props: {
    varientType: {
      type: Number,
      default: 0
    },
    content: {
      type: String,
      default: ''
    },
    isToolTip: {
      type: Boolean,
      default: false
    }
  }
})
</script>
