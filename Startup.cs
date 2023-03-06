using App.Services;
using AppMvc.Net.ExtendMethods;
using AppMvc.Net.Models;
using AppMvc.Net.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AppMvc.Net
{
    public class Startup
    {
        public static string ContentRootPath { get; set; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                string connectString = Configuration.GetConnectionString("AppMvcConnectionStrings");
                options.UseSqlServer(connectString);
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            // dk dich vu productservice, PlanetService
            services.AddSingleton<ProductService>();
            services.AddSingleton<PlanetService>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.AddStatusCodePage(); // Tuy bien Response 


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/sayhi", async (context) =>
                {
                    await context.Response.WriteAsync($"Hello ASP.NET {DateTime.Now}");
                });

                endpoints.MapControllerRoute
                (
                    name: "first",
                    pattern: "{url}/{id?}",
                    defaults: new
                    {
                        controller = "first",
                        action = "ViewProduct"
                    },
                    constraints: new
                    {
                        url = "xemsanpham"
                    }
                );

                endpoints.MapAreaControllerRoute
                (
                    name: "product",
                    areaName: "ProductManage",
                    pattern: "/{controller}/{action=index}/{id?}"
                );


                endpoints.MapControllerRoute
                (
                    name: "default",
                    pattern: "/{controller=Home}/{action=index}/{id?}"
                );



                endpoints.MapRazorPages();
            });
        }
    }
}
