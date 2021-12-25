export interface ISignupViewModel {
  userName : string
  userNameKana: string
  password: string
  mailAddress: string
}

export class SignupViewModel implements ISignupViewModel {
  userName = ''
  userNameKana = ''
  password = ''
  mailAddress = ''
}
