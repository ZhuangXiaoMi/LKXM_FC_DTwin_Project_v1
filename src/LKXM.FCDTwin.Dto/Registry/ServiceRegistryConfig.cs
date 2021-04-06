using System;

namespace LKXM.FCDTwin.Dto
{
    /// <summary>
    /// 服务注册节点
    /// </summary>
    public class ServiceRegistryConfig
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 服务标签(版本)
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// 服务端口号(可选：默认加载启动路径端口)
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务地址(可选：默认加载启动路径)
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string RegistryAddress { get; set; }

        /// <summary>
        /// 服务健康检查地址
        /// </summary>
        public string HealthCheckAddress { get; set; }
    }
}
