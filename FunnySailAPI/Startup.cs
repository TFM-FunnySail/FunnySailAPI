using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();


            #region ServicesCAD

            services.AddScoped<IBoatTypeCAD, BoatTypeCAD>();
            services.AddScoped<IBoatCAD, BoatCAD>();
            services.AddScoped<IBoatResourceCAD, BoatResourceCAD>();
            services.AddScoped<IBoatInfoCAD, BoatInfoCAD>();
            services.AddScoped<IRequiredBoatTitleCAD, RequiredBoatTitleCAD>();
            services.AddScoped<IBoatPricesCAD, BoatPricesCAD>();
            services.AddScoped<IUserCAD, UsersCAD>();
            services.AddScoped<IBookingCAD, BookingCAD>();
            services.AddScoped<IInvoiceLineCAD, InvoiceLineCAD>();
            services.AddScoped<IClientInvoiceCAD, ClientInvoiceCAD>();
            services.AddScoped<IBoatBookingCAD, BoatBookingCAD>();

            #endregion

            #region ServicesCEN

            #endregion

            #region ServicesCP

            #endregion

            #region GeneralServices

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
