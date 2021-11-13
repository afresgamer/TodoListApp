using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB;
using ToDoApp_backend.ViewModel;
using ToDoApp_backend.ViewModel.CategoryMaster;

namespace ToDoApp_backend.DiConfig
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(book => book.BookId, option => option.MapFrom(viewModel => viewModel.Id))
                .ReverseMap();
            CreateMap<CategoryMaster, CategoryMasterViewModel>()
                .ReverseMap();

        }
    }
}
