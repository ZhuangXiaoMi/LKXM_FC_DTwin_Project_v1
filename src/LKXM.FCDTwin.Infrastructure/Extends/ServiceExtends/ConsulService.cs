using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Infrastructure.Cluster;
using LKXM.FCDTwin.Infrastructure.Registry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LKXM.FCDTwin.Infrastructure
{
    /// <summary>
    /// Consul 注册中心扩展(加载配置)
    /// </summary>
    public static class ConsulService
    {
        /// <summary>
        /// Consul 服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulRegistryService(this IServiceCollection services, IConfiguration configuration)
        {
            //1.加载Consul服务注册配置
            services.Configure<ServiceRegistryConfig>(configuration.GetSection("ConsulRegistry"));

            //2.注册Consul注册
            services.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();
            return services;
        }

        /// <summary>
        /// Consul 服务发现
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsulDiscoveryService(this IServiceCollection services/*, IConfiguration configuration*/)
        {
            //1.加载Consul服务发现配置
            //services.Configure<ServiceDiscoveryConfig>(configuration.GetSection("ConsulDiscovery"));

            //2.注册Consul服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
            return services;
        }

        /// <summary>
        /// 添加consul
        /// </summary>
        /// <typeparam name="ConsulHttpClient"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientConsulService<ConsulHttpClient>(this IServiceCollection services) where ConsulHttpClient : class
        {
            //注册consul服务发现
            services.AddConsulDiscoveryService();
            //注册服务负载均衡
            services.AddSingleton<ILoadBalance, RandomLoadBalance>();
            //注册httpclient
            services.AddSingleton<ConsulHttpClient>();
            return services;
        }
    }
}
