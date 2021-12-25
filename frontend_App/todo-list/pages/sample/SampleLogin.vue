
<template>
  <div>
    <b-container fluid="sm">
      <b-form>
        <div class="columns is-mobile">
          <validation-observer v-slot="{ invalid }">
            <div class="field">
              <!-- <label class="label">お名前</label> -->
              <div class="control">
                <base-text-validation v-model="textValue" rules="required" fieldname="お名前" :is-label="true" />
              </div>
            </div>
            <div class="field">
              <!-- <label class="label">メールアドレス</label> -->
              <div class="control">
                <base-text-validation v-model="emailValue" rules="required|email" fieldname="メールアドレス" :is-label="true" />
              </div>
            </div>
            <div class="field">
              <div class="control">
                <b-button class="button is-link" :disabled="invalid">
                  送信する
                </b-button>
                <p v-show="invalid" class="help is-danger">
                  全ての必須項目を入力すると「送信する」ボタンが有効化されます。
                </p>
              </div>
            </div>
          </validation-observer>
        </div>
      </b-form>
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import BaseTextValidation from '../../components/Base/BaseTextValidation.vue'

export default Vue.extend({
  name: 'SampleLogin',
  components: {
    BaseTextValidation
  },
  data () {
    return {
      textValue: '',
      emailValue: ''
    }
  }
})
</script>

<style scoped>
.field {
  margin-top: 10px;
}

.help {
  margin-top: 10px;
}
</style>
