using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Repository.CategoryMaster;
using ToDoApp_backend.ViewModel.CategoryMaster;

namespace ToDoApp_backend.UseCase
{
    public class CategoryMasterUseCase
    {
        private readonly ICategoryMasterRepository _repository;

        public CategoryMasterUseCase(ICategoryMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryMasterViewModel>> GetList()
        {
            var result = await _repository.FetchCategoryMasterListAsync();

            return result;
        }

        public async Task<bool> CreateNewCategoryMaster(CategoryMasterViewModel categoryMaster)
        {
            var result = await _repository.InsertCategoryMaster(categoryMaster);

            return result;
        }

        public async Task<bool> SortCategoryMaster(List<CategoryMasterViewModel> categoryMasters)
        {
            var result = await _repository.SortCategoryMasterList(categoryMasters);

            return result;
        }

        public async Task DeleteCategoryMaster(CategoryMasterViewModel categoryMaster)
        {
            await _repository.DeleteCategoryMaster(categoryMaster);
        }
    }
}
