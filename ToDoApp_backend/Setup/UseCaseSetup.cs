using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp_backend.UseCase;

namespace TodoApp.Setup
{
    public class UseCaseSetup
    {
        public UseCaseSetup(IServiceCollection services)
        {
            services.AddScoped<BookUseCase>();
        }
    }
}
