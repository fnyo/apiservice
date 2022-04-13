using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Middleware
{
    public class FirstMiddleware
    {
        private readonly RequestDelegate _next;

        public FirstMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("First Middleware");
            await _next(context);
            await context.Response.WriteAsync("First Middlerware");
        }
    }

    public static class FirstMiddlewareExtensions
    {
        public static void UseFirst(this IApplicationBuilder app)
        {
           app.UseMiddleware<FirstMiddleware>();
        }
    }
}
