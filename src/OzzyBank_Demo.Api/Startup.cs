using System.IO;
using System.IO.Compression;
using System.Reflection;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OzzyBank_Demo.Api.Middleware;
using Serilog;

namespace OzzyBank_Demo.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            if (env == null) return;

            CurrentEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{CurrentEnvironment.EnvironmentName}.json", true);

            if (builder != null) Config = builder.Build();
        }
        
        private IWebHostEnvironment CurrentEnvironment { get; }

        public IConfiguration Config { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

            services.AddCustomSwagger();
            services.AddSingleton(Log.Logger);
            services.AddHealthChecks();
            services.AddCustomAuthentication(Config);
            services.AddCustomCors();
            services.AddDependencyInjection();
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var basePath = $"/{Constants.ApplicationName}";
            app.UsePathBase(basePath);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSerilogRequestLogging();
            app.UseCustomSwagger(env.EnvironmentName, basePath);
            app.UseCors("AllowPolicy");
            app.UseResponseCompression();
            app.UseAuthentication();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}