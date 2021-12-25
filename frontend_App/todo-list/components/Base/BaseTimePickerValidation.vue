<template>
  <validation-provider v-slot="{ errors }" :rules="rules" :name="fieldname">
    <b-form-datepicker
      v-model="innerValue"
      :placeholder="placeholder"
      today-button
      close-button
      locale="ja"
    />
    <p v-show="errors.length" class="help is-danger">
      {{ errors[0] }}
    </p>
  </validation-provider>
</template>

<script>
export default {
  name: 'BaseTimePickerValidation',
  props: {
    rules: {
      type: String,
      required: true
    },
    value: {
      type: [Date],
      default: new Date()
    },
    fieldname: {
      type: String,
      required: true
    },
    placeholder: {
      type: String,
      default: ''
    }
  },
  computed: {
    innerValue: {
      get () {
        return this.value
      },
      set (val) {
        this.$emit('input', val)
      }
    }
  },
  methods: {}
}
</script>

<style scoped>
.help {
  margin-top: 10px;
  color: red;
}
</style>
