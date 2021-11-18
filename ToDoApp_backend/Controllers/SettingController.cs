using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.Setting;

namespace ToDoApp_backend.Controllers
{
    [Route("setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly SettingUseCase _useCase;
        public SettingController(SettingUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _useCase.GetSetting();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SettingViewModel viewModel)
        {
            var result = await _useCase.PostSetting(viewModel);

            return Ok(result);
        }
    }
}
