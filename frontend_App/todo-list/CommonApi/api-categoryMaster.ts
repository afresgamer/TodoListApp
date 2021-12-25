import request from '~/plugins/request'
import { CategoryMasterViewModel, CategoryMaster } from '~/types'

function GetCategoryMasterList () {
  const path = '/category-master'
  return request.get<CategoryMasterViewModel[]>(path)
}

function GetCategoryMasterDataList () {
  const path = '/category-master/data-list'
  return request.get<CategoryMaster[]>(path)
}

function CreateCategoryMaster (data: CategoryMasterViewModel) {
  const path = '/category-master'
  return request.post<Boolean>(path, { data })
}

function SortCategoryMaster (dataList: CategoryMasterViewModel[]) {
  const path = '/category-master/sort'
  return request.put<Boolean>(path, { data: dataList })
}

function DeleteCategoryMaster (data: CategoryMasterViewModel) {
  const path = '/category-master'
  return request.delete(path, { data })
}

export default {
  GetCategoryMasterList,
  GetCategoryMasterDataList,
  CreateCategoryMaster,
  SortCategoryMaster,
  DeleteCategoryMaster
}
