export interface IBookViewModel {
  BookId: number
  Title: string
  Author: string
}

export class BookViewModel implements IBookViewModel {
  BookId = 0
  Title = ''
  Author = ''
}
