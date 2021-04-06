using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Infrastructure.Registry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace LKXM.FCDTwin.Infrastructure
{
    /// <summary>
    /// 微服务注册发现使用扩展
    /// </summary>
    public static class ConsulMiddleware
    {
        public static IApplicationBuilder UseConsulRegistryConfigure(this IApplicationBuilder app)
        {
            //1.从IOC容器中获取Consul服务注册配置
            var serviceNode = app.ApplicationServices.GetRequiredService<IOptions<ServiceRegistryConfig>>().Value;

            //2.获取应用程序生命周期
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            //2.1获取服务注册实例
            var serviceRegistry = app.ApplicationServices.GetRequiredService<IServiceRegistry>();

            //3.获取服务地址
            var features = app.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            var uri = new Uri(address);

            //4.注册服务
            serviceNode.Id = Guid.NewGuid().ToString();
            serviceNode.Address = $"{uri.Scheme}://{uri.Host}";
            serviceNode.Port = uri.Port;
            serviceNode.HealthCheckAddress = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceNode.HealthCheckAddress}";
            serviceRegistry.Register(serviceNode);

            //5.服务器关闭时注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                serviceRegistry.Deregister(serviceNode);
            });

            return app;
        }
    }
}
