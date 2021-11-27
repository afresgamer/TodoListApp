using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.UseCase;
using ToDoApp_backend.ViewModel.ScheduleList;

namespace ToDoApp_backend.Controllers
{
    [Route("schedule-list")]
    [ApiController]
    public class ScheduleListController : ControllerBase
    {
        private readonly ScheduleListUseCase _useCase;

        public ScheduleListController(ScheduleListUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleListViewModel>>> Get()
        {
            var result = await _useCase.GetScheduleList();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(List<long> scheduleIdList)
        {
            await _useCase.DeleteScheduleList(scheduleIdList);

            return Ok();
        }
    }
}
