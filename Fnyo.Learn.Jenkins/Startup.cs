using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Autofac;
using Fnyo.Learn.Jenkins.Aop;
using Fnyo.Learn.Jenkins.Context;
using Fnyo.Learn.Jenkins.Middleware;
using Fnyo.Learn.Jenkins.Toolkits.Email;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TmsDbContext>(options => options.UseMySql(
                Configuration.GetConnectionString("TmsConnection"), MySqlServerVersion.LatestSupportedServerVersion));
            services.AddControllers();
            services.AddAutoMapper(typeof(MapProfile));

            services.AddHttpContextAccessor();
            #region 配置
            services.Configure<MailHostOption>(Configuration.GetSection("MailHost"));
            #endregion
            // 跨域问题
            services.AddCors(options =>
              options.AddPolicy("mycors", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
            );
            // aop配置全局拦截器
            //services.ConfigureDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<GlobalExceptorAttribute>();
            //});


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fnyo.Learn.Jenkins", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<AutofacModule>();

            // 获取配置
            var redisConfig = Configuration.GetSection("Redis:Default");
          


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fnyo.Learn.Jenkins v1"));
            }
            app.UseCors("mycors");
            app.UseHttpsRedirection();

            // 自定义的中间件
            app.UseDefault();

            //app.UseFirst();


            //app.UseSecond();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
