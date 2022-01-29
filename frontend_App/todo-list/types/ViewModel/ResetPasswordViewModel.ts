
export interface IResetPasswordViewModel {
  userId: number
  password: string
}

export class ResetPasswordViewModel implements IResetPasswordViewModel {
  userId = 0
  password = ''
}
