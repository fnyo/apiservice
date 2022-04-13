using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Middleware
{
    public class SecondMiddleware
    {
        private readonly RequestDelegate _next;

        public SecondMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Second Middleware");
            await _next(context);
            await context.Response.WriteAsync("Second Middleware");
        }

    }

    public static class SecondMiddlewareExtensions
    {
        public static void UseSecond(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SecondMiddleware>();
        }
    }
}
