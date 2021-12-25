
export interface ILoginViewModel {
  userId: number
  password: string
}

export class LoginViewModel implements ILoginViewModel {
  userId: number = 0
  password: string = ''
}
