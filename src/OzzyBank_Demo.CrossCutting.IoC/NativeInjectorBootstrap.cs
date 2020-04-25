using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OzzyBank_Demo.CrossCutting.IoC
{
    public class NativeInjectorBootstrap
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}