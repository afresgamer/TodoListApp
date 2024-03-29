﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp_backend.UseCase;

namespace ToDoApp_backend.Setup
{
    public class UseCaseSetup
    {
        public UseCaseSetup(IServiceCollection services)
        {
            services.AddScoped<BookUseCase>();
            services.AddScoped<SignupUseCase>();
            services.AddScoped<LoginUseCase>();
            services.AddScoped<CategoryMasterUseCase>();
            services.AddScoped<SettingUseCase>();
            services.AddScoped<ScheduleUseCase>();
            services.AddScoped<ScheduleListUseCase>();
            services.AddScoped<CalendarUseCase>();
            services.AddScoped<ResetPasswordUseCase>();
            services.AddScoped<ScreenTransitionUseCase>();
        }
    }
}
