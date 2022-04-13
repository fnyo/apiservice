using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Aop
{
    public class GlobalExceptorAttribute : AbstractInterceptorAttribute
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                Console.WriteLine("服务begin");
                await next(context);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                Console.WriteLine("服务end");
            }
        }
    }
}
