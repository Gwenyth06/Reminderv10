using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Reminderv10.Settings;
using Reminderv10.Models;
using Microsoft.Extensions.Options;
using Reminderv10.Services;

namespace Reminderv10
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<MedicineDatabaseSettings>(
                Configuration.GetSection(nameof(MedicineDatabaseSettings)));

            services.AddSingleton<IMedicineDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MedicineDatabaseSettings>>().Value);

            var mongoDbSettings = Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
                (
                    mongoDbSettings.ConnectionString, mongoDbSettings.Name
                );

            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/Stop/Index";
            });
            services.AddSingleton<MedicineService>();

            services.AddControllers()
       .AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddControllersWithViews();
        }

        /
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
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
            });
        }
    }
}
