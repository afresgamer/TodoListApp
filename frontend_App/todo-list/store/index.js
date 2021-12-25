
export const state = () => ({
  createFlg: false,
  UserId: 0
})

export const mutations = {
  setCreateFlg (state, flg) {
    state.createFlg = flg
    // eslint-disable-next-line no-console
    console.log(state.createFlg)
  },
  setUser (state, userId) {
    state.UserId = userId
    // eslint-disable-next-line no-console
    console.log(state.UserId)
  }
}
