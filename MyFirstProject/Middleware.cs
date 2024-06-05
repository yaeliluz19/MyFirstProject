using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyFirstProject.Controllers;
using Services;
using System.Threading.Tasks;

namespace MyFirstProject
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private IRatingService _ratingService;
        public Middleware(RequestDelegate next)
        {
            
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating r=new Rating();
            r.Host = httpContext.Request.Host.Value;
            r.Method = httpContext.Request.Method;
            r.Path = httpContext.Request.Path;
            r.RecordDate = DateTime.UtcNow;
            r.Referer = httpContext.Request.Headers["Referer"].ToString();
            r.UserAgent = httpContext.Request.Headers["User-Agent"].ToString();

            /*_logger.LogInformation("My Middlware....");
            _ratingService = ratingService;*/
            ratingService.CreateRating(r);
            await  _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class _Extensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
