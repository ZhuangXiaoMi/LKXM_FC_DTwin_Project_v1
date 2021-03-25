using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace LKXM.FCDTwin.Infrastructure
{
    public static class SwaggerService
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;

            services.AddSwaggerGen(options =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(GroupNameEnum).GetFields(BindingFlags.Public | BindingFlags.Static).ToList().ForEach(group =>
                {
                    //获取枚举值上的特性
                    var info = group.GetCustomAttributes(typeof(GroupInfoAttribute), false).OfType<GroupInfoAttribute>().FirstOrDefault();
                    options.SwaggerDoc(group.Name, new OpenApiInfo
                    {
                        Version = Enum.GetName(typeof(ApiVersionEnum), info?.Version),
                        Title = $"FC-DTwin-{info?.Title}-{RuntimeInformation.FrameworkDescription}",
                        Description = info?.Description,
                        Contact = new OpenApiContact
                        {
                            Name = "FC-DTwin",
                            Email = "",
                            Url = new Uri("https://www.baidu.com/")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "FC-DTwin",
                            Url = new Uri("https://www.baidu.com/")
                        }
                    });
                    options.OrderActionsBy(o => o.RelativePath);
                });
                //没有加特性的分到这个NoGroup上
                options.SwaggerDoc("NoGroup", new OpenApiInfo
                {
                    Title = "无分组"
                });
                //判断接口归于哪个分组
                options.DocInclusionPredicate((docName, apiDescription) =>
                {
                    if (docName == "NoGroup")
                    {
                        //当分组为NoGroup时，只要没加特性的都属于这个组
                        return string.IsNullOrEmpty(apiDescription.GroupName);
                    }
                    else
                    {
                        return apiDescription.GroupName == docName;
                    }
                });

                #region 引入XML注释
                try
                {
                    var xmlPath = Path.Combine(basePath, "LKXM.FCDTwin.Api.xml");//项目属性->生成->输出->XML文档文件，对应文件名
                    options.IncludeXmlComments(xmlPath, true);//默认第二个参数是false，这个是controller的注释，改为true

                    var xmlEntityPath = Path.Combine(basePath, "LKXM.FCDTwin.Entity.xml");
                    options.IncludeXmlComments(xmlEntityPath);
                }
                catch (Exception ex)
                {

                }
                #endregion 引入XML注释

                #region Token绑定到ConfigureServices

                // 开启加权小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                // Jwt Bearer 认证，必须是 oauth2
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输)直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion Token绑定到ConfigureServices

            });
        }
    }
}
