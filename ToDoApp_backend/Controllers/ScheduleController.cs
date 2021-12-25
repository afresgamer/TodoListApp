using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.Schedule;

namespace ToDoApp_backend.Controllers
{
    [Route("schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleUseCase _useCase;
        public ScheduleController(ScheduleUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("{scheduleId:long}")]
        public async Task<ActionResult> Get(long scheduleId)
        {
            return Ok(await _useCase.GetSchedule(scheduleId));
        }

        [HttpPost]
        public async Task<ActionResult> Post(ScheduleViewModel scheduleViewModel)
        {
            var result = await _useCase.CreateSchedule(scheduleViewModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(ScheduleViewModel scheduleViewModel)
        {
            var result = await _useCase.UpdateSchedule(scheduleViewModel);

            return Ok(result);
        }

        [HttpDelete("{scheduleId:long}")]
        public async Task<ActionResult> Delete(long scheduleId)
        {
            await _useCase.DeleteSchedule(scheduleId);

            return Ok();
        }
    }
}
