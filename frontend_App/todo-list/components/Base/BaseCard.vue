<template>
  <div>
    <div class="pt-5">
      <b-card img-left class="card">
        <b-row>
          <b-col>
            <b-card-img :src="src" alt="Image" class="rounded-0" />
          </b-col>
          <b-col>
            <a :href="href">{{ name }}</a>
            <b-card-text v-for="(data, index) in cardTexts" :key="index" class="mt-2">
              <b-icon :icon="data.iconText" />{{ data.cardText }}
            </b-card-text>
          </b-col>
        </b-row>
      </b-card>
    </div>
  </div>
</template>

<script lang="ts">
import Vue, { PropType } from 'vue'

interface CardData {
  cardText: string
  iconText: string
}

export default Vue.extend({
  props: {
    src: {
      type: String,
      default: ''
    },
    name: {
      type: String,
      default: ''
    },
    href: {
      type: String,
      default: ''
    },
    cardTexts: {
      type: Array as PropType<CardData[]>,
      default: () => {}
    }
  }
})
</script>

<style scoped>
.card {
  border: 2px solid black;
}
</style>
