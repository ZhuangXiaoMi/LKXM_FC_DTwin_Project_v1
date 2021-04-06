using Consul;
using LKXM.FCDTwin.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Infrastructure.Registry
{
    /// <summary>
    /// Consul 服务发现实现
    /// </summary>
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly IConfiguration _configuration;

        public ConsulServiceDiscovery(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public async Task<IList<ServiceUrl>> Discovery(string serviceName)
        {
            ServiceDiscoveryConfig serviceDiscovery = _configuration.GetSection("ConsulDiscovery").Get<ServiceDiscoveryConfig>();

            //1.创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscovery.RegistryAddress);
            });

            //2.consul查询服务，根据具体的服务名称查询
            var queryResult = await consulClient.Catalog.Service(serviceName);

            //3.将服务进行拼接
            var list = new List<ServiceUrl>();
            foreach (var service in queryResult.Response)
            {
                list.Add(new ServiceUrl { Url = service.ServiceAddress + ":" + service.ServicePort });
            }
            return list;
        }
    }
}
