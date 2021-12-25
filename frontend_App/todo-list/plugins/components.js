import Vue from 'vue'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import BaseBadge from '../components/Base/BaseBadge.vue'
import BaseCard from '../components/Base/BaseCard.vue'
import BaseCheckbox from '../components/Base/BaseCheckBox.vue'
import BaseBreadcrumb from '../components/Base/BaseBreadcrumb.vue'
import BaseSelect from '../components/Base/BaseSelect.vue'
import BaseToolTip from '../components/Base/BaseToolTip.vue'
import BaseTextValidation from '../components/Base/BaseTextValidation.vue'
import BaseTimePickerValidation from '../components/Base/BaseTimePickerValidation.vue'

import CommonNavbar from '../components/BaseLayout/CommonNavbar.vue'
import LoginNavbar from '../components/BaseLayout/LoginNavbar.vue'
import NotLoginNavbar from '../components/BaseLayout/NotLoginNavbar.vue'
import Alert from '../components/Modal/Alert.vue' // テスト

// import alert from '../plugins/alert.js'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)

// Vue.prototype.$alert = alert

Vue.component('BaseBadge', BaseBadge)
Vue.component('BaseCard', BaseCard)
Vue.component('BaseCheckbox', BaseCheckbox)
Vue.component('BaseToolTip', BaseToolTip)
Vue.component('BaseBreadcrumb', BaseBreadcrumb)
Vue.component('BaseSelect', BaseSelect)
Vue.component('BaseTextValidation', BaseTextValidation)
Vue.component('BaseTimePickerValidation', BaseTimePickerValidation)

Vue.component('CommonNavbar', CommonNavbar)
Vue.component('LoginNavbar', LoginNavbar)
Vue.component('NotLoginNavbar', NotLoginNavbar)
Vue.component('Alert', Alert)
