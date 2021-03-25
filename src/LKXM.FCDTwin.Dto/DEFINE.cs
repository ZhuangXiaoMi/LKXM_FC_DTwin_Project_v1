using System.ComponentModel;

namespace LKXM.FCDTwin.Dto
{
    /// <summary>
    /// 状态码定义
    /// </summary>
    public class DEFINE
    {
        #region 接口状态码定义
        #region 1xx：信息，服务器收到请求，需要请求者继续执行操作

        #endregion 1xx：信息，服务器收到请求，需要请求者继续执行操作

        #region 2xx：成功，操作被成功接收并处理
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        public const int SUCCESS = 200;

        /// <summary>
        /// 无内容
        /// </summary>
        [Description("无内容")]
        public const int NO_CONTENT = 204;
        #endregion 2xx：成功，操作被成功接收并处理

        #region 3xx：重定向，需要进一步的操作以完成请求

        #endregion 3xx：重定向，需要进一步的操作以完成请求

        #region 4xx：客户端错误，请求包含语法错误或无法完成请求
        /// <summary>
        /// 错误请求
        /// </summary>
        [Description("错误请求")]
        public const int ERROR_BAD_REQUEST = 400;

        /// <summary>
        /// 未授权
        /// </summary>
        [Description("未授权")]
        public const int ERROR_UNAUTHORIZED = 401;

        /// <summary>
        /// 禁止
        /// </summary>
        [Description("禁止")]
        public const int ERROR_FORBIDDEN = 403;

        /// <summary>
        /// 未找到
        /// </summary>
        [Description("未找到")]
        public const int ERROR_NOT_FOUND = 404;

        /// <summary>
        /// 请求超时
        /// </summary>
        [Description("请求超时")]
        public const int ERROR_REQUEST_TIMEOUT = 408;

        /// <summary>
        /// 冲突
        /// </summary>
        [Description("冲突")]
        public const int ERROR_CONFLICT = 409;

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        public const int ERROR_GONE = 410;

        /// <summary>
        /// 不支持的媒体类型
        /// </summary>
        [Description("不支持的媒体类型")]
        public const int ERROR_UNSUPPORTED_MEDIA_TYPE = 415;
        #endregion 4xx：客户端错误，请求包含语法错误或无法完成请求

        #region 5xx：服务器错误，服务器在处理请求的过程中发生了错误
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        [Description("服务器内部错误")]
        public const int FAIL = 500;
        #endregion 5xx：服务器错误，服务器在处理请求的过程中发生了错误
        #endregion 接口状态码定义
    }
}
