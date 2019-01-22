using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AreaAndPolicy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AreaAndPolicy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ReflectionIT.Mvc.Paging;

namespace AreaAndPolicy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // Custom context feature

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc(config =>
            {
                //config.Filters.Add(new StatusCodeResultFilter());
               // config.Filters.Add(new AuthorizeFilter("AtLeast21"));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireElevatedRights", policy => policy.RequireRole("SuperAdministrator", "ChannelAdministrator"));
                options.AddPolicy("AtLeast21", policy =>
          policy.Requirements.Add(new MinimumAgeRequirement(21)));
            });

            services.AddPaging();
            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseMiddleware<CustomErrorResponseMiddleware>("/home/custom-error-response");
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //app.UseStatusCodePagesWithReExecute("/Error/{0}");
          

            // app.UseExceptionHandler("/Error");
            app.UseMvc(routes =>
            {
               
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
          name: "areas",
          template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );
            });
        }
    }
}
