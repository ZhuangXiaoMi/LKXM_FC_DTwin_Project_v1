using LKXM.FCDTwin.Dto;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Infrastructure
{
    /// <summary>
    /// 微服务中HttpClient熔断，降级策略扩展
    /// </summary>
    public static class PollyService
    {
        /// <summary>
        /// HttpClient扩展方法
        /// </summary>
        /// <param name="services">ioc容器</param>
        /// <param name="name">HttpClient名称(针对不同的服务进行熔断、降级)</param>
        /// <param name="action">熔断降级配置</param>
        /// <returns></returns>
        public static IServiceCollection AddPollyHttpClientService(this IServiceCollection services, string name, Action<PollyHttpClientOptions> action)
        {
            //创建选项配置类
            PollyHttpClientOptions options = new PollyHttpClientOptions();
            action(options);

            //配置httpClient，熔断降级策略
            //超时->重试->熔断->降级，从下往上，下层被上层捕获
            services.AddHttpClient(name)
                //降级策略(捕获异常，进行自定义处理)
                .AddPolicyHandler(Policy<HttpResponseMessage>.HandleInner<Exception>().FallbackAsync(options.HttpResponseMsg
                , async ex =>
                {
                    //降级打印异常
                    Console.WriteLine($"服务{name}开始降级，异常消息：{ex.Exception.Message}");
                    //降级后的数据
                    Console.WriteLine($"服务{name}降级内容响应：{options.HttpResponseMsg.Content.ToString()}");
                    await Task.CompletedTask;
                }))
                //断路器策略，熔断机制，设置3次异常开启熔断，10s后下一个请求重新请求微服务，否则不请求微服务直接返回熔断器自定义异常信息
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().CircuitBreakerAsync(options.CircuitBreakerOpenFallCount, TimeSpan.FromSeconds(options.CircuitBreakerDownTime)
                , (ex, ts) =>
                {
                    Console.WriteLine($"服务{name}断路器开启，异常消息：{ex.Exception.Message}");
                    Console.WriteLine($"服务{name}断路器开启时间：{ts.TotalSeconds}s");
                }
                , () =>
                {
                    Console.WriteLine($"服务{name}断路器关闭");
                }
                , () =>
                {
                    //每过10s关闭熔断，下个请求微服务，之后重新打开
                    Console.WriteLine($"服务{name}断路器半开启(时间控制，自动开关)");
                }))
                //重试策略，失败重试
                .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<Exception>().RetryAsync(options.RetryCount))
                //超时策略
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(options.TimeoutTime)));

            return services;
        }
    }
}
