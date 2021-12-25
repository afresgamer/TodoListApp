<template>
  <div>
    <b-container fluid="lg">
      <b-table straped hover :items="BookList" />
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import CommonApi from '../../CommonApi'
import { BookViewModel } from '../../types'

interface Data {
  BookList: BookViewModel[]
}

export default Vue.extend({
  components: {},
  data (): Data {
    return {
      BookList: []
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    async getInitData (): Promise<void> {
      this.BookList = await CommonApi.ApiBook.getBookList()
    }
  }
})
</script>
