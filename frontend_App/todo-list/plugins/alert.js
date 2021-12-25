
export default function alert (message = '') {
  this.$bvModal.msgBoxOk(message, {
    title: 'メッセージ',
    size: 'sm',
    buttonSize: 'sm',
    headerClass: 'p-2 border-bottom-0',
    footerClass: 'p-2 border-top-0',
    centered: true
  })
}
