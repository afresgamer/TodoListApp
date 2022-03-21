using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;

namespace ToDoApp_backend.Controllers
{
    [Route("screen-transition")]
    [ApiController]
    public class ScreenTransitionController : ControllerBase
    {
        private readonly ScreenTransitionUseCase _useCase;

        public ScreenTransitionController(ScreenTransitionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<bool> Get(string toPath)
        {
            return await _useCase.SiteAuthorizationCheck(toPath);
        }
    }
}
