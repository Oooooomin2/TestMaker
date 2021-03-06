using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using DDD.Domain.Model.Interface;
using DDD.Domain.Model.Repository.Home;
using DDD.Domain.Data;
using DDD.Domain.Models.Repository;
using DDD.Domain.Model.Interface.Home;
using DDD.Domain.Model.Interface.Users;
using DDD.Domain.Model.Interface.Tests;
using DDD.Domain.Model.Interface.Accounts;
using DDD.Domain.Models.Repository.Tests;
using DDD.Domain.Model.Repository.Users;
using DDD.Domain.Model.Repository.Accounts;
using CoreMvcAutoMapper;
using AutoMapper;

namespace TestMaker
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
            services.AddControllersWithViews();
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => !context.User.Identity.IsAuthenticated;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.Cookie.Name = "TestMaker";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = new PathString("/Account/Denied");
                    options.ReturnUrlParameter = "ReturnUrl";
                });
            services.AddDbContext<TestMakerContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("TestMakerContext")));
            services.AddTransient<IHomeRepository, HomeRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(cfg => {
                cfg.AddProfile<AutoMapperConfig>();
            });
            services.AddSingleton<IMapper, Mapper>();
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

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
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
