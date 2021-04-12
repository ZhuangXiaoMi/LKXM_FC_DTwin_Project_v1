using LKXM.FCDTwin.Dto;
using LKXM.FCDTwin.Infrastructure.Cluster;
using LKXM.FCDTwin.Infrastructure.Registry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Infrastructure.Registry
{
    /// <summary>
    /// Consul HttpClient扩展
    /// </summary>
    public class ConsulHttpClient
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly ILoadBalance _loadBalance;
        private readonly IHttpClientFactory _httpClientFactory;

        public ConsulHttpClient(IServiceDiscovery serviceDiscovery, ILoadBalance loadBalance, IHttpClientFactory httpClientFactory)
        {
            _serviceDiscovery = serviceDiscovery;
            _loadBalance = loadBalance;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get 方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceSchme">服务名称(http/https)</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务路径</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string serviceSchme, string serviceName, string serviceLink
            , Func<T, T> filterFunc = null)
        {
            //获取服务
            IList<ServiceUrl> serviceUrls = await _serviceDiscovery.Discovery(serviceName);
            //负载均衡服务
            ServiceUrl serviceUrl = _loadBalance.Select(serviceUrls);
            //建立请求
            HttpClient httpClient = _httpClientFactory.CreateClient("fcdtwin");
            HttpResponseMessage httpResponse = await httpClient.GetAsync(serviceUrl.Url + serviceLink);

            //json转换成对象
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string json = await httpResponse.Content.ReadAsStringAsync();
                T data = JsonConvert.DeserializeObject<T>(json);
                if (filterFunc != null)
                {
                    data = filterFunc.Invoke(data);
                }
                return data;
            }
            else
            {
                throw new Exception($"{serviceName}服务调用错误");
            }
        }
    }
}
