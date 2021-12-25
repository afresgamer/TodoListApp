<template>
  <div v-if="!isSwitch">
    <b-form-checkbox :value="value" name="checkbox">
      {{ description }}
    </b-form-checkbox>
  </div>
  <div v-else>
    <b-form-checkbox :value="value" name="check-button" switch>
      {{ description }}
    </b-form-checkbox>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  components: {},
  props: {
    isSwitch: {
      type: Boolean,
      default: false
    },
    value: {
      type: Boolean,
      default: false
    },
    description: {
      type: String,
      default: ''
    }
  }
})
</script>
