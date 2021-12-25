
export interface ILoginResultViewModel {
  userId: number
  Token: string
}

export class LoginResultViewModel implements ILoginResultViewModel {
  userId: number = 0
  Token: string = ''
}
