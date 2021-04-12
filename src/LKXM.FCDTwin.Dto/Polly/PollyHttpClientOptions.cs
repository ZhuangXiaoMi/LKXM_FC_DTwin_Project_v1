using System.Net.Http;

namespace LKXM.FCDTwin.Dto
{
    public class PollyHttpClientOptions
    {
        /// <summary>
        /// 超时时间设置，单位秒
        /// </summary>
        public int TimeoutTime { get; set; }

        /// <summary>
        /// 请求失败，重试请求次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 执行多少次异常，开启断路器(如：失败2次，重试次数不列入计算，开启断路器)
        /// 如：RetryCount=2，CircuitBreakerOpenFallCount=3，中间会发送6次请求
        /// </summary>
        public int CircuitBreakerOpenFallCount { get; set; }

        /// <summary>
        /// 断路器开启时间(如：设置2秒，断路器2秒后自动由开启到关闭)
        /// </summary>
        public int CircuitBreakerDownTime { get; set; }

        /// <summary>
        /// 降级处理(将异常消息封装成正常消息返回，然后进行响应处理，如：系统繁忙)
        /// </summary>
        public HttpResponseMessage HttpResponseMsg { get; set; }
    }
}
