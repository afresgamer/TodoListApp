using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Repository.Schedule;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.ScheduleList;

namespace ToDoApp_backend.UseCase
{
    public class ScheduleListUseCase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IConfiguration _configuration;

        public ScheduleListUseCase(IScheduleRepository scheduleRepository, IConfiguration configuration)
        {
            _scheduleRepository = scheduleRepository;
            _configuration = configuration;
        }

        public async Task<List<ScheduleListViewModel>> GetScheduleList()
        {
            var filePath = Path.Combine(
               _configuration.GetValue<string>("LoginInfoFilePath"),
               _configuration.GetValue<string>("LoginInfoFileName")
           );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _scheduleRepository.FetchScheduleListAsync(userInfo.UserId);

            return result;
        }

        public async Task DeleteScheduleList(List<long> scheduleIdList) 
        {
            await _scheduleRepository.DeleteScheduleList(scheduleIdList);
        }
    }
}
