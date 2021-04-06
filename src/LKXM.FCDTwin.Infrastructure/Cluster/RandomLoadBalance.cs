using LKXM.FCDTwin.Dto;
using System;
using System.Collections.Generic;

namespace LKXM.FCDTwin.Infrastructure.Cluster
{
    /// <summary>
    /// 随机负载均衡
    /// 其他：加权轮询
    /// </summary>
    public class RandomLoadBalance : AbsLoadBalance
    {
        private readonly Random random = new Random();

        public override ServiceUrl DoSelect(IList<ServiceUrl> serviceUrls)
        {
            //随机选择服务连接
            var index = random.Next(serviceUrls.Count);
            return serviceUrls[index];
        }
    }
}
