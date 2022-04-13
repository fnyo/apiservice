using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Middleware
{
    /// <summary>
    /// 中间件
    /// </summary>
    public class DefaultMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DefaultMiddleware> _logger;

        /// <summary>
        /// 通过构造函数注入RequestDelegate委托
        /// </summary>
        /// <param name="next"></param>
        public DefaultMiddleware(RequestDelegate next,ILogger<DefaultMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 处理上下文
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation($"请求路径：{httpContext.Request.Path.ToString()}");
            return _next(httpContext);
            //_logger.LogInformation($"响应码:{httpContext.Response.StatusCode.ToString()}");
            //return Task.CompletedTask;
        }
    }
    /// <summary>
    /// 拓展函数的形式注册管道中间件
    /// </summary>
    public static class DefaultMiddlewareExtensions
    {
        public static IApplicationBuilder UseDefault(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<DefaultMiddleware>();
        }
    }
}
