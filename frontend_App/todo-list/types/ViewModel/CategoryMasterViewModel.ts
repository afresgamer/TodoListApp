export interface ICategoryMasterViewModel {
  SortNo: number
  CategoryName: string
  Summary: string
}

export class CategoryMasterViewModel implements ICategoryMasterViewModel {
  SortNo = 0
  CategoryName = ''
  Summary = ''
}
