using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Helpers;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using FunnySailAPI.Infrastructure.Initialize;
using FunnySailAPI.SwaggerIdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
                .AddRoles<IdentityRole>()
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
            services.AddScoped<IBoatBookingCAD, BoatBookingCAD>();
            services.AddScoped<IActivityBookingCAD, ActivityBookingCAD>();
            services.AddScoped<IInvoiceLineCAD, InvoiceLineCAD>();
            services.AddScoped<IClientInvoiceCAD, ClientInvoiceCAD>();
            services.AddScoped<IServiceCAD, ServiceCAD>();
            services.AddScoped<IServiceBookingCAD, ServiceBookingCAD>();
            services.AddScoped<IPortCAD, PortCAD>();
            services.AddScoped<IMooringCAD, MooringCAD>();
            services.AddScoped<IReviewCAD, ReviewCAD>();
            services.AddScoped<IActivityCAD, ActivityCAD>();
            services.AddScoped<IActivityBookingCAD, ActivityBookingCAD>();
            services.AddScoped<IResourcesCAD, ResourcesCAD>();
            services.AddScoped<ITechnicalServiceCAD, TechnicalServiceCAD>();
            services.AddScoped<ITechnicalServiceBoatCAD, TechnicalServiceBoatCAD>();
            services.AddScoped<IOwnerInvoiceCAD, OwnerInvoiceCAD>();
            services.AddScoped<IOwnerInvoiceLineCAD, OwnerInvoiceLineCAD>();
            services.AddScoped<IRefundCAD, RefundCAD>();

            #endregion

            #region ServicesCEN

            services.AddScoped<IRequiredBoatTitlesCEN, RequiredBoatTitlesCEN>();
            services.AddScoped<IBoatCEN, BoatCEN>();
            services.AddScoped<IBoatInfoCEN, BoatInfoCEN>();
            services.AddScoped<IBoatPricesCEN, BoatPricesCEN>();
            services.AddScoped<IBoatResourceCEN, BoatResourceCEN>();
            services.AddScoped<IBoatTypeCEN, BoatTypeCEN>();
            services.AddScoped<IBoatBaseCEN, BoatBaseCEN>();
            services.AddScoped<IReviewCEN, ReviewCEN>();
            services.AddScoped<IUserCEN, UserCEN>();
            services.AddScoped<IActivityCEN, ActivityCEN>();
            services.AddScoped<IPortCEN, PortCEN>();
            services.AddScoped<IResourcesCEN, ResourcesCEN>();
            services.AddScoped<IMooringCEN, MooringCEN>();
            services.AddScoped<IServiceCEN, ServiceCEN>();
            services.AddScoped<ITechnicalServiceCEN, TechnicalServiceCEN>();
            services.AddScoped<IBookingCEN, BookingCEN>();
            services.AddScoped<IActivityBookingCEN, ActivityBookingCEN>();
            services.AddScoped<IServiceBookingCEN, ServiceBookingCEN>();
            services.AddScoped<IBoatBookingCEN, BoatBookingCEN>();
            services.AddScoped<IInvoiceLineCEN, InvoiceLineCEN>();
            services.AddScoped<IClientInvoiceCEN, ClientInvoiceCEN>();
            services.AddScoped<IOwnerInvoiceLineCEN, OwnerInvoiceLineCEN>();
            services.AddScoped<IBoatBaseCEN, BoatBaseCEN>();
            services.AddScoped<IRefundCEN, RefundCEN>();
            services.AddScoped<IOwnerInvoiceCEN, OwnerInvoiceCEN>();

            #endregion

            #region ServicesCP

            services.AddScoped<IBoatCP, BoatCP>();
            services.AddScoped<ITechnicalServiceCP, TechnicalServiceCP>();
            services.AddScoped<IPortMooringCP, PortMooringCP>();
            services.AddScoped<IBookingCP, BookingCP>();
            services.AddScoped<IRefundCP, RefundCP>();
            services.AddScoped<IOwnerInvoiceCP, OwnerInvoiceCP>();
            #endregion

            #region GeneralServices
            services.AddScoped<IDatabaseTransactionFactory, DatabaseTransactionFactory>();
            services.AddScoped<IInitializeDB, InitializeDB>();
            services.AddScoped<IOwnerInvoiceTypeFactory, OwnerInvoiceTypeFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FunnySail API",
                    Version = "v1",
                    Description = "Api para administración y venta de servicios, actividades y embarcaciones.",
                    Contact = new OpenApiContact
                    {
                        Name = "Admin funnySail",
                        Email = string.Empty
                    },
                });

                options.DocumentFilter<IdentityServerDocumentFilter>("");
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryIdentityResources(OauthConfig.IdentityResources)
            .AddInMemoryApiScopes(OauthConfig.ApiScopes)
            .AddInMemoryClients(OauthConfig.Clients)
            .AddAspNetIdentity<ApplicationUser>();

            services.AddAuthentication();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInitializeDB initializeDB)
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

            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapMiddlewareGet("/.well-known/openid-configuration", appBuilder =>
            {
                appBuilder.UseMiddleware<OpenAPIMiddleware>();
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FunnySail API V1");
                c.SwaggerEndpoint("/.well-known/openid-configuration", "Identity Server V4");
                

            // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
            c.RoutePrefix = "swagger";
            });

            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            initializeDB.Initialize();

            app.UseIdentityServer();    

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
