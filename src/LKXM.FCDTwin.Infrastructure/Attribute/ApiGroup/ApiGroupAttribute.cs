using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;

namespace LKXM.FCDTwin.Infrastructure
{
    /// <summary>
    /// 系统分组特性
    /// </summary>
    public class ApiGroupAttribute : Attribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 分组名称，IApiDescriptionGroupNameProvider 接口属性
        /// </summary>
        public string GroupName { get; set; }

        public ApiGroupAttribute(GroupNameEnum name)
        {
            GroupName = Enum.GetName(typeof(GroupNameEnum), name);
        }
    }
}
