import request from '~/plugins/request'
import { BookViewModel } from '~/types'

async function getBookList () {
  const path = '/book'
  const result = await request.get<BookViewModel[]>(path)
  return result
}

export default {
  getBookList
}
