using System.ComponentModel;

namespace LKXM.FCDTwin.Dto
{
    public class ResultResDto
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [Description("状态码")]
        public int code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [Description("返回信息")]
        public string msg { get; set; }

        public ResultResDto()
        {
            code = DEFINE.SUCCESS;
            msg = "请求成功";
        }
    }

    public class ResultResDto<T> : ResultResDto
    {
        public T data { get; set; }
    }
}
