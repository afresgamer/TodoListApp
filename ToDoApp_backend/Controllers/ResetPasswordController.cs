using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.Login;

namespace ToDoApp_backend.Controllers
{
    [Route("reset-password")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly ResetPasswordUseCase _useCase;

        public ResetPasswordController(ResetPasswordUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPut]
        public async Task<ActionResult> Put(ResetPasswordViewModel resetPasswordViewModel)
        {
            return Ok(await _useCase.ResetPasswordAsync(resetPasswordViewModel));
        }
    }
}
