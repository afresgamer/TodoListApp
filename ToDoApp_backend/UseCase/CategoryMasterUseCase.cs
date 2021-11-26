using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Repository.CategoryMaster;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.CategoryMaster;

namespace ToDoApp_backend.UseCase
{
    public class CategoryMasterUseCase
    {
        private readonly ICategoryMasterRepository _repository;
        private readonly IConfiguration _configuration;

        public CategoryMasterUseCase(ICategoryMasterRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<List<CategoryMasterViewModel>> GetList()
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _repository.FetchCategoryMasterListAsync(userInfo.UserId);

            return result;
        }

        public async Task<List<DB.CategoryMaster>> GetCategoryMastersAsync()
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _repository.FetchCategoryMaster(userInfo.UserId);

            return result;
        }

        public async Task<bool> CreateNewCategoryMaster(CategoryMasterViewModel categoryMaster)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _repository.InsertCategoryMaster(categoryMaster, userInfo.UserId);

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
