using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OzzyBank_Demo.Api.Security;
using OzzyBank_Demo.CrossCutting.IoC;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace OzzyBank_Demo.Api
{
    public static class Setup
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = Constants.ApplicationName,
                    Description = Constants.ApplicationName,
                    Contact = new OpenApiContact {Name = "Brazilian Team"}
                });
                
                c.AddFluentValidationRules();
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app, string environment,
            string basePath)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"../swagger/v1/swagger.json", Constants.ApplicationName);
                c.DocumentTitle = $"{Constants.ApplicationName} - {environment}";
            });
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/";
                    options.LoginPath = "/";
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetSection("TokenAuthentication:Issuer").Value,
                        ValidAudience = configuration.GetSection("TokenAuthentication:Audience").Value,
                        IssuerSigningKey = JwtSecurityKey.Create(configuration.GetSection("TokenAuthentication:SecurityKey").Value)
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Log.Error("Authentication Failed - {Message}", context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Log.Information("Token Validated - {SecurityToken}", context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowPolicy",
                    policy => policy.AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                );
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            NativeInjectorBootstrap.RegisterServices(services);
        }
    }
}