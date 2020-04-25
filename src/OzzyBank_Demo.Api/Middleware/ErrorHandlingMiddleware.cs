using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OzzyBank_Demo.Domain.Exceptions;
using Serilog;

namespace OzzyBank_Demo.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(context, ex);
                Log.Error($"Occured an error: {ex.Message}");
            }
        }
        
        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var errors = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    errors = JsonConvert.SerializeObject(validationException.Errors
                        .Select(f => new
                        {
                            f.ErrorMessage
                        }).ToList());
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;

            if (string.IsNullOrEmpty(errors))
            {
                errors = JsonConvert.SerializeObject(new {error = exception.Message});
            }

            context.Response.WriteAsync(errors);
        }
    }
}