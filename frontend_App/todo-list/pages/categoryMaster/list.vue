<template>
  <div>
    <base-breadcrumb :breadcrumb-items="breadcrumbItems" />
    <b-container>
      <h2>カテゴリーマスタ一覧</h2>
      <div class="btn-area">
        <category-master-details @refresh="getInitData" />
        <b-button class="float-right" variant="primary" @click="sortDataList()">
          並び替え
        </b-button>
      </div>
      <div class="category-table">
        <b-table striped hover :items="items" :fields="fields">
          <template #cell(削除)="data">
            <b-button variant="primary" @click="deleteData(data.item)">
              <b-icon icon="trash-fill" /> 削除
            </b-button>
          </template>
        </b-table>
      </div>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import CategoryMasterDetails from '../../components/Modal/CategoryMasterDetails.vue'
import { CategoryMasterViewModel } from '../../types'
import CommonApi from '../../CommonApi'

interface Data {
  breadcrumbItems: any
  fields: any
  items: CategoryMasterViewModel[]
}

export default Vue.extend({
  components: { CategoryMasterDetails },
  data (): Data {
    return {
      breadcrumbItems: [
        {
          text: 'ホーム',
          href: '/home'
        },
        {
          text: 'カテゴリーマスタ一覧',
          active: true
        }
      ],
      fields: [
        { key: 'SortNo', label: '並び順' },
        { key: 'CategoryName', label: 'カテゴリー名' },
        { key: 'Summary', label: '概要説明' },
        '削除'
      ],
      items: []
    }
  },
  created () {
    this.getInitData()
  },
  methods: {
    async getInitData (): Promise<void> {
      this.items = await CommonApi.ApiCategoryMaster.GetCategoryMasterList()
    },
    deleteData (data: CategoryMasterViewModel): void {
      this.$bvModal.msgBoxConfirm('削除してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async () => {
        await CommonApi.ApiCategoryMaster.DeleteCategoryMaster(data).then(() => {
          this.$bvModal.msgBoxOk('削除完了しました。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          }).then(() => {
            this.getInitData()
          })
        })
      })
    },
    sortDataList (): void {
      this.$bvModal.msgBoxConfirm('並び替えしてもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then(async () => {
        await CommonApi.ApiCategoryMaster.SortCategoryMaster(this.items).then(() => {
          this.$bvModal.msgBoxOk('並び替え完了しました。', {
            title: 'メッセージ',
            size: 'md',
            buttonSize: 'sm',
            headerClass: 'p-2 border-bottom-0',
            footerClass: 'p-2 border-top-0',
            centered: true
          }).then(() => {
            this.getInitData()
          })
        })
      })
    }
  }
})
</script>

<style>
.btn-area {
  margin-top: 20px;
}

.table.b-table.table-striped {
  margin-top: 80px;
}
</style>
