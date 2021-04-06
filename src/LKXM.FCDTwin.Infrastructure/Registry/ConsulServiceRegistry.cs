using Consul;
using LKXM.FCDTwin.Dto;
using System;

namespace LKXM.FCDTwin.Infrastructure.Registry
{
    /// <summary>
    /// Consul 服务注册实现
    /// </summary>
    public class ConsulServiceRegistry : IServiceRegistry
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceRegistry"></param>
        public void Register(ServiceRegistryConfig serviceRegistry)
        {
            //1.创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceRegistry.RegistryAddress);
            });

            //2.创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = serviceRegistry.Id,
                Name = serviceRegistry.Name,
                Address = serviceRegistry.Address,
                Port = serviceRegistry.Port,
                // 安全检查，心跳机制，每隔一段请求一次保证安全
                Check = new AgentServiceCheck
                {
                    // 3.1 consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2 服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3 consul健康检查地址，微服务心跳检测地址
                    HTTP = serviceRegistry.HealthCheckAddress,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10)
                }
            };

            //3.注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            //4.关闭连接
            consulClient.Dispose();
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceRegistry"></param>
        public void Deregister(ServiceRegistryConfig serviceRegistry)
        {
            //1.创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceRegistry.RegistryAddress);
            });

            //2.注销服务
            consulClient.Agent.ServiceDeregister(serviceRegistry.Id);

            //3.关闭连接
            consulClient.Dispose();
        }
    }
}
