using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.ViewModel.Login;
using ToDoApp_backend.UseCase;

namespace ToDoApp_backend.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginUseCase _useCase;

        public LoginController(LoginUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPut]
        public async Task<ActionResult> Put(LoginViewModel loginViewModel)
        {
            var result = await _useCase.Login(loginViewModel);

            if (result == 0) NotFound();

            return Ok(result);
        }

        [HttpPut("logout")]
        public async Task<ActionResult> Logout()
        {
            var result = await _useCase.Logout();

            return Ok(result);
        }
    }
}
