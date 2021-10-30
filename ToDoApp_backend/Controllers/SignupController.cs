using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.ViewModel.Signup;
using ToDoApp_backend.UseCase;

namespace ToDoApp_backend.Controllers
{
    [Route("signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly SignupUseCase _useCase;
        public SignupController(SignupUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("init-create")]
        public async Task<ActionResult> Post(SignupViewModel viewModel)
        {
            var result = await _useCase.CreateNewUser(viewModel);

            return Ok(result);
        }
    }
}
