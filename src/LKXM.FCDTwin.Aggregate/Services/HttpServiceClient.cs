using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LKXM.FCDTwin.Infrastructure.Registry;
using LKXM.FCDTwin.Infrastructure.Cluster;
using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.Dto;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace LKXM.FCDTwin.Aggregate.Services
{
    /// <summary>
    /// 服务调用实现
    /// </summary>
    public class HttpServiceClient : IServiceClient
    {
        public readonly IServiceDiscovery _serviceDiscovery;
        public readonly ILoadBalance _loadBalance;
        public readonly IHttpClientFactory _httpClientFactory;
        private readonly string ServiceSchme = "http";
        private readonly string ServiceName = "service";
        private readonly string ServiceLink = "/api";

        public HttpServiceClient(IServiceDiscovery serviceDiscovery, ILoadBalance loadBalance, IHttpClientFactory httpClientFactory)
        {
            _serviceDiscovery = serviceDiscovery;
            _loadBalance = loadBalance;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<TTest>> GetTest()
        {
            //获取服务
            IList<ServiceUrl> serviceUrls = await _serviceDiscovery.Discovery(ServiceName);
            //负载均衡服务
            ServiceUrl serviceUrl = _loadBalance.Select(serviceUrls);
            //建立请求
            HttpClient httpClient = _httpClientFactory.CreateClient("fcdtwin");
            HttpResponseMessage httpResponse  = await httpClient.GetAsync(serviceUrl + ServiceLink);

            IList<TTest> tests = null;
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                string json = await httpResponse.Content.ReadAsStringAsync();
                tests = JsonConvert.DeserializeObject<List<TTest>>(json);
            }

            return tests;
        }
    }
}
