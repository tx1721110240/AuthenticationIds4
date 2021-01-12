using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();//�����ռ�dll-������--action--PartManager
            #region IdentityServer4--Client
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:7200";//ids4�ĵ�ַ
                    options.ApiName = "UserApi";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(options =>
            {//�Զ������ֻ���Policy = "eMailPolicy" ��Ч
                options.AddPolicy("eMailPolicy",
                    policyBuilder => policyBuilder
                    .RequireAssertion(context =>
                    context.User.HasClaim(c => c.Type == "client_eMail")
                    && context.User.Claims.First(c => c.Type.Equals("client_eMail")).Value.EndsWith("@qq.com")));//Client
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(
                new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
                });

            app.UseRouting();
            #region  Ids4
            app.UseAuthentication();
            #endregion
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
