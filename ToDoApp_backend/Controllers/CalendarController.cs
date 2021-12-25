using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.Calendar;

namespace ToDoApp_backend.Controllers
{
    [Route("calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarUseCase _useCase;
        public CalendarController(CalendarUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("{calendarId:long}")]
        public async Task<ActionResult> Get(long calendarId)
        {
            return Ok(await _useCase.GetCalendar(calendarId));
        }

        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await _useCase.GetCalendarList());
        }

        [HttpPost]
        public async Task<ActionResult> Post(CalendarEventViewModel scheduleViewModel)
        {
            var result = await _useCase.CreateCalendar(scheduleViewModel);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(CalendarEventViewModel scheduleViewModel)
        {
            var result = await _useCase.UpdateCalendar(scheduleViewModel);

            return Ok(result);
        }

        [HttpDelete("{calendarId:long}")]
        public async Task<ActionResult> Delete(long calendarId)
        {
            await _useCase.DeleteCalendar(calendarId);

            return Ok();
        }
    }
}
