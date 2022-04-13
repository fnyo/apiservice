using Autofac.Extras.DynamicProxy;
using Fnyo.Learn.Jenkins.Aop;
using Fnyo.Learn.Jenkins.Interceptors;
using Fnyo.Learn.Service;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Services
{
    [Intercept(typeof(LogInterceptor))]
    public class HelloAopService:IDependency,IHelloAopService
    {
        //[GlobalExceptor]
        //public async virtual Task<string> SayHello()
        //{
        //    await Task.Delay(2000);
        //    return "Hello World";
        //}

        public async Task<string> Castle()
        {
            await Task.Delay(1000);
            return "complete";
        }
    }
}
