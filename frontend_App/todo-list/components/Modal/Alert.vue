<template>
  <div>
    <b-button v-b-modal.baseModal>
      {{ btnValue }}
    </b-button>
    <b-modal
      id="baseModal"
      size="sm"
      centered
      :title="title"
      :header-class="headerClass"
      :footer-class="footerClass"
      ok-only
    >
      <p>
        {{ message }}
      </p>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  props: {
    btnValue: {
      type: String,
      default: ''
    },
    title: {
      type: String,
      default: ''
    },
    message: {
      type: String,
      default: ''
    }
  },
  data () {
    return {
      footerClass: 'p-2 border-top-0',
      headerClass: 'p-2 border-bottom-0'
    }
  }
})
</script>
