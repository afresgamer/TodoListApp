using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp_backend.Repository;
using ToDoApp_backend.Repository.User;
using ToDoApp_backend.Repository.License;
using ToDoApp_backend.Repository.CategoryMaster;
using ToDoApp_backend.Repository.Setting;
using ToDoApp_backend.Repository.Schedule;
using ToDoApp_backend.Repository.Calendar;

namespace ToDoApp_backend.Setup
{
    public class RepositorySetup
    {
        public RepositorySetup(IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILicenseRepository, LicenseRepository>();
            services.AddTransient<ICategoryMasterRepository, CategoryMasterRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<ICalendarRepository, CalendarRepository>();
        }
    }
}
