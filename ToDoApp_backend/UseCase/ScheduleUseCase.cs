using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using ToDoApp_backend.Repository.Schedule;
using ToDoApp_backend.ViewModel.Schedule;
using ToDoApp_backend.Utility;
using ToDoApp_backend.Auth;

namespace ToDoApp_backend.UseCase
{
    public class ScheduleUseCase
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IConfiguration _configuration;

        public ScheduleUseCase(IScheduleRepository scheduleRepository, IConfiguration configuration)
        {
            _scheduleRepository = scheduleRepository;
            _configuration = configuration;
        }

        public async Task<ScheduleViewModel> GetSchedule(long scheduleId)
        {
            var filePath = Path.Combine(
               _configuration.GetValue<string>("LoginInfoFilePath"),
               _configuration.GetValue<string>("LoginInfoFileName")
           );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _scheduleRepository.FindSchedule(scheduleId, userInfo.UserId);

            return result;
        }

        public async Task<long> CreateSchedule(ScheduleViewModel viewModel)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _scheduleRepository.InsertSchedule(viewModel, userInfo.UserId);

            return result;
        }

        public async Task<bool> UpdateSchedule(ScheduleViewModel viewModel)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _scheduleRepository.UpdateSchedule(viewModel, userInfo.UserId);

            return result;
        }

        public async Task DeleteSchedule(long scheduleId)
        {
            await _scheduleRepository.DeleteSchedule(scheduleId);
        }
    }
}
