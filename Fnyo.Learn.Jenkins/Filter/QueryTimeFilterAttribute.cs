using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace Fnyo.Learn.Jenkins.Filter
{
    public class QueryTimeFilterAttribute : Attribute,IActionFilter
    {


        private const string DURATION = "DURATION";

        /// <summary>
        /// action执行后
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var stopwatch = context.RouteData.Values[DURATION] as Stopwatch;
            stopwatch.Stop();
            var time = stopwatch.Elapsed;
            var log = $"Host-{context.HttpContext.Request.Host},Path-{context.HttpContext.Request.Path}总耗时:{ time.Milliseconds}ms";
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(log);
        }


        /// <summary>
        /// action执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            context.RouteData.Values.Add(DURATION, stopwatch);
        }
    }
}
