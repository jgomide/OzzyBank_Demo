using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OzzyBank_Demo.Domain.Interfaces.Repository;
using OzzyBank_Demo.Domain.Interfaces.Service;
using OzzyBank_Demo.Repository;
using OzzyBank_Demo.Service;


namespace OzzyBank_Demo.Api.Installers
{
    public class DIInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOzzyBankDatabase, OzzyBankDatabase>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            
            //services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();

        }
    }
}

