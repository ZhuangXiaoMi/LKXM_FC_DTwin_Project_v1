using LKXM.FCDTwin.Entity;
using LKXM.FCDTwin.Infrastructure.Registry;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Aggregate.Services
{
    /// <summary>
    /// 服务调用实现
    /// </summary>
    public class HttpServiceClient : IServiceClient
    {
        private readonly ConsulHttpClient _consulHttpClient;
        private readonly string ServiceSchme = "http";
        private readonly string ServiceName = "fcdtwin";
        private readonly string ServiceLink = "/api/t_test/list";

        public HttpServiceClient(ConsulHttpClient consulHttpClient)
        {
            _consulHttpClient = consulHttpClient;
        }

        public async Task<List<TTest>> GetTest()
        {
            List<TTest> tests = null;
            try
            {
                tests = await _consulHttpClient.GetAsync<List<TTest>>(ServiceSchme, ServiceName, ServiceLink);
            }
            catch (Exception ex)
            {

            }
            return tests;
        }
    }
}
