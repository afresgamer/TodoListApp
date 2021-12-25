import createPersistedState from 'vuex-persistedstate'

export default ({ store }) => {
  createPersistedState({
    key: 'yourkey',
    paths: ['UserId'],
    storage: window.sessionStorage
  })(store)
}
