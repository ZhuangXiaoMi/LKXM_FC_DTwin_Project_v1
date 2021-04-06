using LKXM.FCDTwin.Dto;

namespace LKXM.FCDTwin.Infrastructure.Registry
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public interface IServiceRegistry
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceRegistry"></param>
        void Register(ServiceRegistryConfig serviceRegistry);

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceRegistry"></param>
        void Deregister(ServiceRegistryConfig serviceRegistry);
    }
}
