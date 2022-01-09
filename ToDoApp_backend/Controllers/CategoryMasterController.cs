using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.CategoryMaster;

namespace ToDoApp_backend.Controllers
{
    [Route("category-master")]
    [ApiController]
    public class CategoryMasterController : ControllerBase
    {
        private readonly CategoryMasterUseCase _useCase;

        public CategoryMasterController(CategoryMasterUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _useCase.GetList();

            return Ok(result);
        }

        [HttpGet("data-list")]
        public async Task<ActionResult> GetDataList()
        {
            var result = await _useCase.GetCategoryMastersAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<bool> Post(CategoryMasterViewModel categoryMaster)
        {
            return await _useCase.CreateNewCategoryMaster(categoryMaster);
        }

        [HttpPut("sort")]
        public async Task<bool> PutSort(List<CategoryMasterViewModel> categoryMasters)
        {
            return await _useCase.SortCategoryMaster(categoryMasters);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(CategoryMasterViewModel categoryMaster)
        {
            await _useCase.DeleteCategoryMaster(categoryMaster);

            return Ok();
        }
    }
}
