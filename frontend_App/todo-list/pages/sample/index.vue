<template>
  <div>
    <b-container fluid="sm">
      <h1>Sample</h1>
      <div>
        <b-form v-if="show" @submit="onSubmit" @reset="onReset">
          <b-form-group
            id="input-group-1"
            label="Email address:"
            label-for="input-1"
            description="We'll never share your email with anyone else."
          >
            <b-form-input
              id="input-1"
              v-model="form.email"
              type="email"
              placeholder="Enter email"
              required
            />
          </b-form-group>

          <b-form-group id="input-group-2" label="Your Name:" label-for="input-2">
            <b-form-input
              id="input-2"
              v-model="form.name"
              placeholder="Enter name"
              required
            />
          </b-form-group>

          <b-form-group id="input-group-3" label="Food:" label-for="input-3">
            <b-form-select
              id="input-3"
              v-model="form.food"
              :options="foods"
              required
            />
          </b-form-group>

          <b-form-group id="input-group-4" v-slot="{ ariaDescribedby }">
            <b-form-checkbox-group
              id="checkboxes-4"
              v-model="form.checked"
              :aria-describedby="ariaDescribedby"
            >
              <b-form-checkbox value="me">
                Check me out
              </b-form-checkbox>
              <b-form-checkbox value="that">
                Check that out
              </b-form-checkbox>
            </b-form-checkbox-group>
          </b-form-group>

          <b-button type="submit" variant="primary">
            Submit
          </b-button>
          <b-button type="reset" variant="danger">
            Reset
          </b-button>
          <div>
            <alert btn-value="テスト" title="メッセージ" message="入力内容をご確認ください。" />
          </div>
          <div class="mb-2">
            <b-button @click="showConfirm02">
              テスト02
            </b-button>
          </div>
          <div>
            <b-button @click="showConfirm02">
              テスト03
            </b-button>
            <b-button @click="showConfirm02">
              テスト04
            </b-button>
            {{ show02 }}
          </div>
          <div>
            <b-button @click="$bvModal.show('modal-scoped')">
              Open Modal
            </b-button>
          </div>
        </b-form>
      </div>
      <test-model />
    </b-container>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import TestModel from '../../components/Modal/TestModel.vue'

export default Vue.extend({
  components: { TestModel },
  data () {
    return {
      form: {
        email: '',
        name: '',
        food: null,
        checked: []
      },
      foods: [{ text: 'Select One', value: null }, 'Carrots', 'Beans', 'Tomatoes', 'Corn'],
      show: true,
      show02: false,
      confirm: false
    }
  },
  methods: {
    onSubmit (): void {},
    onReset (): void {
      this.form.email = ''
      this.form.name = ''
      this.form.food = null
      this.form.checked = []
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    },
    showConfirm02 (): void {
      this.$bvModal.msgBoxConfirm('登録してもよろしいでしょうか？', {
        title: 'メッセージ',
        size: 'sm',
        buttonSize: 'sm',
        headerClass: 'p-2 border-bottom-0',
        footerClass: 'p-2 border-top-0',
        centered: true
      }).then((value: boolean) => {
        this.show02 = value
      })
    }
  }
})
</script>

<style>

</style>
