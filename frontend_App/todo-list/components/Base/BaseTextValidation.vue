<template>
  <validation-provider v-slot="{ errors }" :rules="rules" :name="fieldname">
    <b-form-textarea
      v-if="isTextArea && !isLabel"
      id="textarea"
      v-model="innerValue"
      :placeholder="fieldname"
      :disabled="disabled"
      rows="3"
      max-rows="6"
    />
    <b-form-input
      v-else-if="!isLabel"
      v-model="innerValue"
      :placeholder="fieldname"
      :type="type"
      :disabled="disabled"
      required
    />
    <b-form-group
      v-else
      :label="fieldname"
      label-for="input"
    >
      <b-form-input
        id="input"
        v-model="innerValue"
        :placeholder="fieldname"
        :type="type"
        :disabled="disabled"
      />
    </b-form-group>
    <p v-show="errors.length" class="help is-danger">
      {{ errors[0] }}
    </p>
  </validation-provider>
</template>

<script>
export default {
  name: 'BaseTextValidation',
  props: {
    rules: {
      type: String,
      required: true
    },
    value: {
      type: [String, Number],
      default: ''
    },
    fieldname: {
      type: String,
      required: true
    },
    isLabel: {
      type: Boolean,
      default: false
    },
    isTextArea: {
      type: Boolean,
      default: false
    },
    type: {
      type: String,
      default: 'text'
    },
    disabled: {
      type: Boolean,
      default: false
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
  }
}
</script>

<style scoped>
.help {
  margin-top: 10px;
  color: red;
}
</style>
