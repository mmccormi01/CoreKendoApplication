using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Serilog;
using CoreKendoApplicationService;
using CoreKendoApplicationWeb.Models;
using static CoreKendoApplicationWeb.Context;


namespace CoreKendoApplicationWeb
{
    public class Startup
    {
        private const int MIN_PASSWORD_LENGTH = 4;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Tell the system how to find the logging framework
            // and other settings for the application are loaded
            // from the appsettings.json file
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Create Serilog globally-shared logger instance
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            LogHelper.Info("Application started");
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            bool UseInMemoryDatabase = Configuration.GetSection("DevSettings:UseInMemoryDatabase").Value != null && bool.Parse(Configuration.GetSection("DevSettings:UseInMemoryDatabase").Value);

            /*
             * Startup with an in-memory database for initial development or else use a SQL server instance.
             *
             * This is controlled by DevSettings:UseInMemoryDatabase in appsettings.json
             */
            if (UseInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(config => config.UseInMemoryDatabase("MemoryBaseDataBase"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDatabaseConnection")));
            }
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = MIN_PASSWORD_LENGTH;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            //services.AddAuthentication("CookieAuthentication")
            //.AddCookie("CookieAuthentication", config =>
            //{
            //    config.Cookie.Name = "UserLoginCookie";
            //    config.LoginPath = "/Login/UserLogin";
            //});
            //services.AddAuthorization();

            //services.AddRazorPages();
            services.AddDirectoryBrowser();
            services.AddMvc()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider isp)
        {
            //app.UseHttpsRedirection();
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
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
            if (Configuration.GetSection("DevSettings:SeedTestUser").Value != null && bool.Parse(Configuration.GetSection("DevSettings:SeedTestUser").Value))
            {
                var userManager = isp.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = isp.GetRequiredService<RoleManager<IdentityRole>>();

                var userName = Configuration.GetSection("DevSettings:TestUser").Value;

                var adminRole = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult resultCreateRole = roleManager.CreateAsync(adminRole).Result;

                if (resultCreateRole.Succeeded)
                {
                    var testUser = new ApplicationUser
                    {
                        Id = userName,
                        UserName = userName,
                        CommonName = "Test User"
                    };

                    var createUserResult = userManager.CreateAsync(testUser, Identity.UserConstants.NO_PASSWORD).Result;

                    if (createUserResult.Succeeded)
                    {
                        _ = userManager.AddToRoleAsync(testUser, "Admin").Result;
                    }
                }
            }
        }
    }
}
