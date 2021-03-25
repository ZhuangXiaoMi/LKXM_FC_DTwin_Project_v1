using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Reflection;

namespace LKXM.FCDTwin.Infrastructure
{
    public class AutofacService : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Autofac
            try
            {
                var basePath = AppContext.BaseDirectory;
                var serviceDllFile = Path.Combine(basePath, "LKXM.FCDTwin.Service.dll");//获取注入项目绝对路径
                var repositoryDllFile = Path.Combine(basePath, "LKXM.FCDTwin.Repository.PostgreSQL.dll");
                if (!File.Exists(serviceDllFile) || !File.Exists(repositoryDllFile))
                {
                    throw new Exception("LKXM.FCDTwin.Service.dll 或 LKXM.FCDTwin.Repository.PostgreSQL.dll 不存在");
                }

                var assemblyService = Assembly.LoadFrom(serviceDllFile);//加载指定文件，会加载引用的其它dll
                builder.RegisterAssemblyTypes(assemblyService)
                    .AsImplementedInterfaces()
                    .InstancePerDependency()
                    .EnableInterfaceInterceptors();

                var assemblyRepository = Assembly.LoadFrom(repositoryDllFile);
                builder.RegisterAssemblyTypes(assemblyRepository)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
            }
            catch (Exception ex)
            {
                //throw new Exception("缺少dll");
            }
            #endregion Autofac

            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor));
        }
    }
}
