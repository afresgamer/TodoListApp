<template>
  <div class="breadcrumb-main-area">
    <div v-if="breadcrumbItems.length <= 0">
      <b-breadcrumb>
        <b-breadcrumb-item active>
          Home
        </b-breadcrumb-item>
      </b-breadcrumb>
    </div>
    <div v-else>
      <b-breadcrumb :items="breadcrumbItems" />
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'

export default Vue.extend({
  components: { },
  props: {
    breadcrumbItems: {
      type: Array,
      default: () => []
    }
  }
})
</script>

<style>
.breadcrumb-main-area {
  margin-left: 80px;
  width: 90%;
}
</style>
