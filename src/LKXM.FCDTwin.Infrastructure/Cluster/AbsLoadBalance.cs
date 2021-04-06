using LKXM.FCDTwin.Dto;
using System.Collections.Generic;

namespace LKXM.FCDTwin.Infrastructure.Cluster
{
    /// <summary>
    /// 负载均衡抽象实现
    /// </summary>
    public abstract class AbsLoadBalance : ILoadBalance
    {
        public ServiceUrl Select(IList<ServiceUrl> serviceUrls)
        {
            if (serviceUrls == null || serviceUrls.Count == 0)
                return null;
            if (serviceUrls.Count == 1)
                return serviceUrls[0];
            return DoSelect(serviceUrls);
        }

        /// <summary>
        /// 子类去实现
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        public abstract ServiceUrl DoSelect(IList<ServiceUrl> serviceUrls);
    }
}
