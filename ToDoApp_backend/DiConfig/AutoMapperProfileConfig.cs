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

        }
    }
}
