using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Capstone
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class VisitorCountMiddleware
    {
        private readonly RequestDelegate _next;

        public VisitorCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {


            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class VisitorCountMiddlewareExtensions
    {
        public static IApplicationBuilder UseVisitorCountMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VisitorCountMiddleware>();
        }
    }
}
