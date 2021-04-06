using LKXM.FCDTwin.Dto;
using Microsoft.AspNetCore.Builder;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LKXM.FCDTwin.Infrastructure
{
    public static class SwaggerMiddleware
    {
        public static void UseSwaggerConfigure(this IApplicationBuilder app, Func<Stream> streamHtml)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //遍历ApiGroupNames所有枚举值生成接口文档
                typeof(GroupNameEnum).GetFields(BindingFlags.Public | BindingFlags.Static).ToList().ForEach(group =>
                {
                    //获取枚举值上的特性
                    var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerEndpoint($"/swagger/{group.Name}/swagger.json", info != null ? info.Title : group.Name);
                    options.IndexStream = streamHtml;
                    options.RoutePrefix = "";

                });
                options.SwaggerEndpoint("/swagger/NoGroup/swagger.json", "无分组");
            });
        }
    }
}
