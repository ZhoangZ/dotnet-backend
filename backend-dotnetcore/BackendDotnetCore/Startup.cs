using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BackendDotnetCore.Helpers;
using BackendDotnetCore.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using BackendDotnetCore.Middleware;
using System.Net.Http;

namespace BackendDotnetCore
{
    class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<HttpClient, HttpClient>();
            services.AddSingleton<AccountDAO, AccountDAO>();
            services.AddSingleton<Product2DAO, Product2DAO>();
            services.AddSingleton<PaymentDAO, PaymentDAO>();
            services.AddSingleton<UserDAO, UserDAO>();


            services.AddCors();
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
                cfg.Cookie.Name = "shareimage";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);    // Thời gian tồn tại của Session
            });
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddSingleton<IUserService, UserService>();
            //services.AddTransient(AccountDAO, AccountDAO);
            services.AddControllersWithViews();
        }

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

            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

            app.UseRouting();
            //app.UseAuthorization();
            app.UseSession();
            app.UseMiddleware<JwtMiddleware>();
            app.UseCorsMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


           


        }
    }
}
