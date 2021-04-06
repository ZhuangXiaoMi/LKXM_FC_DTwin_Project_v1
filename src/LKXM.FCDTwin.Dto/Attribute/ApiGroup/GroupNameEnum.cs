using System;

namespace LKXM.FCDTwin.Dto
{
    /// <summary>
    /// 系统分组枚举
    /// </summary>
    public enum GroupNameEnum
    {
        //[Display(Name = "系统接口", GroupName = "v1", Description = "")]
        [GroupInfo(Title = "系统接口", Version = ApiVersionEnum.v1, Description = "")]
        System,

        //[Display(Name = "测试接口", GroupName = "v1", Description = "")]
        [GroupInfo(Title = "测试接口", Version = ApiVersionEnum.v1, Description = "")]
        Test,

    }
}
