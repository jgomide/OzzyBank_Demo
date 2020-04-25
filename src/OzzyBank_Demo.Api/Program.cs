using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace OzzyBank_Demo.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            ConfigureLogging();

            try
            {
                Log.Information("Starting Host");
                CreateHostBuilder(args)
                    .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
                    .Build()
                    .Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Filter.ByExcluding("RequestPath like '%/healthcheck%'")
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", Constants.ApplicationName)
                .Enrich.WithProperty("System", Constants.SystemName)
                .Enrich.WithProperty("Version", typeof(Program).Assembly.GetName().Version?.ToString())
                .WriteTo.Console(new JsonFormatter())
                .CreateLogger();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog();
    }
}