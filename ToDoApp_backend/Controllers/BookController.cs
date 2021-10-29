using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;

namespace ToDoApp_backend.Controllers
{
    [Route("book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookUseCase _useCase;

        public BookController(BookUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _useCase.Get());
        }
    }
}
