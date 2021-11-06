using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp_backend.Repository;
using ToDoApp_backend.Repository.User;
using ToDoApp_backend.Repository.License;

namespace ToDoApp_backend.Setup
{
    public class RepositorySetup
    {
        public RepositorySetup(IServiceCollection services)
        {
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILicenseRepository, LicenseRepository>();
        }
    }
}
