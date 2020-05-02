using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OzzyBank_Demo.Domain.Interfaces.Repository;
using OzzyBank_Demo.Domain.Interfaces.Service;
using OzzyBank_Demo.Repository;
using OzzyBank_Demo.Service;

namespace OzzyBank_Demo.CrossCutting.IoC
{
    public class NativeInjectorBootstrap
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //services.AddScoped<IDatabaseConfiguration, DatabaseConfiguration>();
            
            services.AddScoped<IOzzyBankDatabase, OzzyBankDatabase>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
        }
    }
}