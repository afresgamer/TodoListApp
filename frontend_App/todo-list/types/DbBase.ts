export interface IDbBase {
  Id: number
  CreateDate: Date
  UpdateDate: Date
}

export abstract class DbBase implements IDbBase {
  Id = 0
  CreateDate = new Date()
  UpdateDate = new Date()
}
