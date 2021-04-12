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
            #region Policy �۶Ͻ��� + Consul
            //�Զ����쳣����(�û��洦��)
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent("ϵͳ��æ�����Ժ�����"),// �Զ����쳣���ݻ򻺴�����
                StatusCode = HttpStatusCode.GatewayTimeout// 504 ״̬��
            };
            services.AddPollyHttpClientService("fcdtwin", options =>
            {
                options.TimeoutTime = 1;
                options.RetryCount = 3;
                options.CircuitBreakerOpenFallCount = 2;
                options.CircuitBreakerDownTime = 100;
                options.HttpResponseMsg = fallbackResponse;
            }).AddHttpClientConsulService<ConsulHttpClient>();

            ////ע��restful api����
            ////services.AddHttpClient("fcdtwin").AddHttpClientConsulService<ConsulHttpClient>();
            ////��ʱ->����->�۶�->�������������ϣ��²㱻�ϲ㲶��
            //services.AddHttpClient("fcdtwin")
            //    //����(�����쳣�������Զ��崦��)
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().FallbackAsync(fallbackResponse
            //    , async ex =>
            //    {
            //        //������ӡ�쳣
            //        Console.WriteLine($"��ʼ�������쳣��Ϣ��{ex.Exception.Message}");
            //        //�����������
            //        //Console.WriteLine($"����������Ӧ��{}");
            //        await Task.CompletedTask;
            //    }))
            //    //�۶ϻ��ƣ�����3���쳣�����۶ϣ�10s����һ��������������΢���񣬷�������΢����ֱ�ӷ����۶����Զ����쳣��Ϣ
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(3, TimeSpan.FromSeconds(10)
            //    , (ex, ts) =>
            //    {
            //        Console.WriteLine($"��·���������쳣��Ϣ��{ex.Exception.Message}");
            //        Console.WriteLine($"��·������ʱ�䣺{ts.TotalSeconds}s");
            //    }
            //    , () =>
            //    {
            //        Console.WriteLine($"��·������");
            //    }
            //    , () =>
            //    {
            //        //ÿ��10s�ر��۶ϣ��¸�����΢����֮�����´�
            //        Console.WriteLine($"��·���뿪��(һ�Ὺ��һ���)");
            //    }))
            //    //ʧ������
            //    .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().RetryAsync(3))
            //    //��ʱ
            //    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3))); 
            #endregion Policy �۶Ͻ��� + Consul

            //ע�����
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
