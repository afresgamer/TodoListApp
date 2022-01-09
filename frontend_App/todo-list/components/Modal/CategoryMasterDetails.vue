<template>
  <div>
    <b-button v-b-modal.baseModal class="float-left" variant="primary">
      新規登録
    </b-button>
    <b-modal
      id="baseModal"
      centered
      hide-footer
      :title="title"
      @show="reset"
      @hidden="reset"
    >
      <validation-observer v-slot="{ invalid }">
        <b-button class="float-right" variant="primary" :disabled="invalid" @click="register">
          登録
        </b-button>
        <b-form class="mt-5">
          <b-form-group
            id="input-group-1"
            label-for="input-1"
          >
            <label class="label">カテゴリー名 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="item.CategoryName" rules="required|min:2|max:16" fieldname="カテゴリー名" />
          </b-form-group>
          <b-form-group
            id="input-group-2"
            label-for="textarea"
          >
            <label class="label">概要説明 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="item.Summary" rules="required|min:2|max:50" fieldname="概要説明" :is-text-area="true" />
          </b-form-group>
          <b-form-group
            id="input-group-3"
            label-for="input-3"
          >
            <label class="label">並び順 <base-badge :varient-type="4" /></label>
            <base-text-validation v-model="item.SortNo" rules="required|min:1|max:4" fieldname="並び順" />
          </b-form-group>
        </b-form>
      </validation-observer>
    </b-modal>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import { CategoryMasterViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  title: string
  item: CategoryMasterViewModel
}

export default Vue.extend({
  components: { },
  data (): Data {
    return {
      title: 'カテゴリーマスタ登録',
      item: new CategoryMasterViewModel()
    }
  },
  methods: {
    register (): void {
      if (this.item.SortNo <= 0) { return }

      this.$bvModal.msgBoxConfirm('登録してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async (messageResult: boolean) => {
        if (!messageResult) { return }
        this.$nuxt.$loading.start()
        const result = await CommonApi.ApiCategoryMaster.CreateCategoryMaster(this.item)
        this.$nuxt.$loading.finish()
        const message = result ? '登録完了しました。' : '登録失敗しました。'
        this.$bvModal.msgBoxOk(message, {
          title: 'メッセージ',
          size: 'md',
          buttonSize: 'sm',
          headerClass: 'p-2 border-bottom-0',
          footerClass: 'p-2 border-top-0',
          centered: true
        }).then(() => {
          if (message) {
            this.close()
            this.$bvModal.hide('baseModal')
          }
        })
      })
    },
    reset (): void {
      this.item = new CategoryMasterViewModel()
    },
    close (): void {
      this.$emit('refresh')
    }
  }
})
</script>

<style>

</style>
