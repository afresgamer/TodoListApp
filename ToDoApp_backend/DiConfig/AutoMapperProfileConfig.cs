using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB;
using ToDoApp_backend.ViewModel;
using ToDoApp_backend.ViewModel.CategoryMaster;
using ToDoApp_backend.ViewModel.Setting;
using ToDoApp_backend.ViewModel.Schedule;
using ToDoApp_backend.ViewModel.ScheduleList;
using ToDoApp_backend.ViewModel.Calendar;

namespace ToDoApp_backend.DiConfig
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(viewModel => viewModel.BookId, option => option.MapFrom(book => book.Id))
                .ReverseMap();
            CreateMap<CategoryMaster, CategoryMasterViewModel>()
                .ReverseMap();
            CreateMap<Setting, SettingViewModel>()
                .ReverseMap();
            CreateMap<Schedule, ScheduleViewModel>()
                .ForMember(viewModel => viewModel.ScheduleName, option => option.MapFrom(schedule => schedule.Name))
                .ReverseMap();
            CreateMap<Schedule, ScheduleListViewModel>()
                .ForMember(viewModel => viewModel.ScheduleName, option => option.MapFrom(schedule => schedule.Name))
                .ForMember(viewModel => viewModel.ScheduleId, option => option.MapFrom(schedule => schedule.Id))
                .ForMember(viewModel => viewModel.StartDay, option => option.MapFrom(schedule => schedule.StartDay.ToString("yyyy/MM/dd")))
                .ForMember(viewModel => viewModel.EndDay, option => option.MapFrom(schedule => schedule.EndDay.ToString("yyyy/MM/dd")))
                .ForMember(viewModel => viewModel.UsageTime, option => option.MapFrom(schedule => schedule.UsageTime.ToString() + "時間"))
                .ReverseMap();
            CreateMap<Calendar, CalendarEventViewModel>()
                .ReverseMap();

        }
    }
}
