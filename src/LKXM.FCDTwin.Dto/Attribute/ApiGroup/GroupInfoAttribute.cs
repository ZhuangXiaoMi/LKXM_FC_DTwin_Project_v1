using System;
using System.ComponentModel;

namespace LKXM.FCDTwin.Dto
{
    /// <summary>
    /// 系统分组枚举信息特性
    /// </summary>
    public class GroupInfoAttribute : Attribute
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Description("标题")]
        public string Title { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [Description("版本号")]
        public ApiVersionEnum Version { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public string Description { get; set; }
    }
}
