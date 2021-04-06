using LKXM.FCDTwin.Dto;
using System.Collections.Generic;

namespace LKXM.FCDTwin.Infrastructure.Cluster
{
    /// <summary>
    /// 服务负载均衡
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        ServiceUrl Select(IList<ServiceUrl> serviceUrls);
    }
}
