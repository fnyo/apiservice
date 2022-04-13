using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Fnyo.Learn.Jenkins.Interceptors;
using Fnyo.Learn.Jenkins.Toolkits.Cahce;
using Fnyo.Learn.Jenkins.Toolkits.Mongodb;
using System.Reflection;

namespace Fnyo.Learn.Jenkins
{
    public class AutofacModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(x => new LogInterceptor());

            Assembly assembly = Assembly.Load("Fnyo.Learn.Service");
            builder.RegisterAssemblyTypes(assembly).Where(t => t.GetInterface("IDependency") != null)
                .EnableClassInterceptors()
                .InstancePerDependency();


            Assembly assembly1 = Assembly.Load("Fnyo.Learn.Jenkins");
            builder.RegisterAssemblyTypes(assembly1).Where(t => t.GetInterface("IDependency") != null)
                 .EnableClassInterceptors()
                .InstancePerDependency();


            builder.RegisterAssemblyTypes(assembly1).Where(t => t.GetInterface("ISingleton") != null)
                .EnableClassInterceptors()
                .SingleInstance();


            builder.RegisterType<RedisHelper>().SingleInstance();

            // 属性注入
            builder.RegisterType<MongodbHelper>().As<MongodbHelper>().SingleInstance().PropertiesAutowired();

            builder.RegisterDynamicProxy();
        }
    }
}
