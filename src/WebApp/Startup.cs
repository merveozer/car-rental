using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.Concrete;
using Application.Infrastructure.Persistence;
using Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using Domain.Constants;

namespace WebApp
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
            services.AddApplicationServices();
            services.AddCors(options =>
                             options.AddDefaultPolicy(builder =>
                             builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            services.AddAuthentication(AuthenticationConstants.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.Name = "Rentacar";
                    o.ExpireTimeSpan = new TimeSpan(0, 10, 0);    //belli bir süre sonra (bu örnek için 0 saat 10 dakika 0 saniye sonra) çýkýþ yapýp oturumu kapatýyor...
                    o.LoginPath = "/auth/login";

                });
        }

        private int VehicleBrandService()
        {
            throw new NotImplementedException();
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
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
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
