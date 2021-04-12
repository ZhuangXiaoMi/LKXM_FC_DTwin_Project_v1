using LKXM.FCDTwin.Aggregate.Services;
using LKXM.FCDTwin.Infrastructure;
using LKXM.FCDTwin.Infrastructure.Cluster;
using LKXM.FCDTwin.Infrastructure.Registry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Aggregate
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Policy 熔断降级 + Consul
            //自定义异常处理(用缓存处理)
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent("系统繁忙，请稍后重试"),// 自定义异常内容或缓存数据
                StatusCode = HttpStatusCode.GatewayTimeout// 504 状态码
            };
            services.AddPollyHttpClientService("fcdtwin", options =>
            {
                options.TimeoutTime = 1;
                options.RetryCount = 3;
                options.CircuitBreakerOpenFallCount = 2;
                options.CircuitBreakerDownTime = 100;
                options.HttpResponseMsg = fallbackResponse;
            }).AddHttpClientConsulService<ConsulHttpClient>();

            ////注册restful api调用
            ////services.AddHttpClient("fcdtwin").AddHttpClientConsulService<ConsulHttpClient>();
            ////超时->重试->熔断->降级，从下往上，下层被上层捕获
            //services.AddHttpClient("fcdtwin")
            //    //降级(捕获异常，进行自定义处理)
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().FallbackAsync(fallbackResponse
            //    , async ex =>
            //    {
            //        //降级打印异常
            //        Console.WriteLine($"开始降级，异常消息：{ex.Exception.Message}");
            //        //降级后的数据
            //        //Console.WriteLine($"降级内容响应：{}");
            //        await Task.CompletedTask;
            //    }))
            //    //熔断机制，设置3次异常开启熔断，10s后下一个请求重新请求微服务，否则不请求微服务直接返回熔断器自定义异常信息
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(3, TimeSpan.FromSeconds(10)
            //    , (ex, ts) =>
            //    {
            //        Console.WriteLine($"断路器开启，异常消息：{ex.Exception.Message}");
            //        Console.WriteLine($"断路器开启时间：{ts.TotalSeconds}s");
            //    }
            //    , () =>
            //    {
            //        Console.WriteLine($"断路器重置");
            //    }
            //    , () =>
            //    {
            //        //每过10s关闭熔断，下个请求微服务，之后重新打开
            //        Console.WriteLine($"断路器半开启(一会开，一会关)");
            //    }))
            //    //失败重试
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().RetryAsync(3))
            //    //超时
            //    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3))); 
            #endregion Policy 熔断降级 + Consul

            //注册服务
            services.AddSingleton<IServiceClient, HttpServiceClient>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
