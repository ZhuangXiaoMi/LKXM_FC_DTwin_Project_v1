using Autofac;
using LKXM.FCDTwin.Infrastructure;
using LKXM.FCDTwin.Repository.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LKXM.FCDTwin.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();//.AddInject();

            services.AddDbContext<ApiDbContext>(p =>
            {
                p.UseNpgsql(Configuration["ConnectionStrings:FCDTwinDbContext"], p => p.CommandTimeout(10));
            });
            services.AddSwaggerService();
            services.AddConsulRegistryService(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerConfigure(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LKXM.FCDTwin.Api.index.html"));
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages();
            //app.UseConsulRegistryConfigure();
            app.UseRouting();
            //app.UseInject();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
