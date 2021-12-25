import { DbBase } from '../DbBase'

export interface ICategoryMaster {
  UserId: number
  CategoryName: string
  Summary: string
  SortNo: number
}

export class CategoryMaster extends DbBase implements ICategoryMaster {
  Id = 0
  CreateDate = new Date()
  UpdateDate = new Date()
  UserId = 0
  CategoryName = ''
  Summary = ''
  SortNo = 0
}
