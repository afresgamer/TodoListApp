using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TodoApp.Setup
{
    public class DIMain
    {
        private readonly IServiceCollection _services;
        public DIMain(IServiceCollection services)
        {
            _services = services;
        }

        public void Run()
        {
            _ = new RepositorySetup(_services);
            _ = new UseCaseSetup(_services);
        }
    }
}
