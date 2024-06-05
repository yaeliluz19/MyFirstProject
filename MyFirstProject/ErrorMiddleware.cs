using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyFirstProject.Controllers;
using System.Threading.Tasks;

namespace MyFirstProject
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public  async Task Invoke(HttpContext httpContext, ILogger<ErrorMiddleware> logger)
        {
            try { 
                await _next(httpContext);
            }
            catch(Exception ex) { 
            _logger.LogInformation($"ErrorMiddleware: {ex.Message}");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync("interenal error in server");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}
