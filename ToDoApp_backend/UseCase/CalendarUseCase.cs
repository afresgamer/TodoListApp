using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.Auth;
using ToDoApp_backend.Repository.Calendar;
using ToDoApp_backend.Utility;
using ToDoApp_backend.ViewModel.Calendar;

namespace ToDoApp_backend.UseCase
{
    public class CalendarUseCase
    {
        private readonly ICalendarRepository _calendarRepository;
        private readonly IConfiguration _configuration;

        public CalendarUseCase(ICalendarRepository calendarRepository ,IConfiguration configuration)
        {
            _configuration = configuration;
            _calendarRepository = calendarRepository;
        }

        public async Task<CalendarEventViewModel> GetCalendar(long calendarId)
        {
            var filePath = Path.Combine(
               _configuration.GetValue<string>("LoginInfoFilePath"),
               _configuration.GetValue<string>("LoginInfoFileName")
           );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _calendarRepository.FindCalendarAsync(calendarId, userInfo.UserId);

            return result;
        }

        public async Task<List<CalendarEventViewModel>> GetCalendarList()
        {
            var filePath = Path.Combine(
               _configuration.GetValue<string>("LoginInfoFilePath"),
               _configuration.GetValue<string>("LoginInfoFileName")
           );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _calendarRepository.FetchCalendarListAsync(userInfo.UserId);

            return result;
        }

        public async Task<bool> CreateCalendar(CalendarEventViewModel viewModel)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _calendarRepository.InsertCalendar(viewModel, userInfo.UserId);

            return result;
        }

        public async Task<bool> UpdateCalendar(CalendarEventViewModel viewModel)
        {
            var filePath = Path.Combine(
                _configuration.GetValue<string>("LoginInfoFilePath"),
                _configuration.GetValue<string>("LoginInfoFileName")
            );

            var userInfo = JsonUtility.JsonDeserialize<UserSession>(filePath);

            var result = await _calendarRepository.UpdateCalendar(viewModel, userInfo.UserId);

            return result;
        }

        public async Task DeleteCalendar(long scheduleId)
        {
            await _calendarRepository.DeleteCalendar(scheduleId);
        }
    }
}
