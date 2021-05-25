using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jlw.Utilities.Data.DbUtility;
using MartinCostello.SqlLocalDb;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Jlw.Extensions.Identity.Mock;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using TUser = Jlw.Extensions.Identity.Stores.ModularBaseUser;

namespace Jlw.Web.Core31.LocalizedContent.SampleWebApp
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AddMockedUsers();
            services.AddIdentityMocking<TUser>();

            services.AddSqlLocalDB(options =>
            {
                options.AutomaticallyDeleteInstanceFiles = true;
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });
            services.AddSqlLocalDbInstance(options =>
            {
                options.InstanceName = Configuration.GetConnectionString("LocalDbInstanceName");
            });
            string connString = "";
            services.AddSingleton<IModularDbClient>( provider =>
            {
                var dbInstanceInfo = provider.GetRequiredService<ISqlLocalDbInstanceInfo>();
                connString = dbInstanceInfo.GetConnectionString();
                return new ModularDbClient<SqlConnection, SqlCommand, SqlParameter, SqlConnectionStringBuilder>();
            });

            services.AddLocalizedContentFieldRepository(options => options.ConnectionString = connString);
            services.AddLocalizedContentTextRepository(options => options.ConnectionString = connString);
            services.AddLocalizedGroupDataItemRepository(options => options.ConnectionString = connString);

            //var provider = services.BuildServiceProvider();

            var mvcBuilder = services.AddControllersWithViews();

            if (Env.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            
            mvcBuilder.AddNewtonsoftJson(o =>
            {
                /*
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                */
            });
            

            services.AddLocalizedContentAdmin();
            services.AddAuthorization(options =>
            {
                options.AddDefaultLocalizedContentAdminAuthorizationPolicy();

                options.AddPolicy("ContentOverrideAdmin", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(claim => claim.Type.Equals("ContentOverrideAccess", StringComparison.InvariantCultureIgnoreCase));
                    });
                });


            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();    // Initializes MS Identity Authentication Middleware
            app.UseAuthorization();     // Initialized MS Identity Authorization Middleware

            app.UseSqlLocalDbInstance();

            app.UseDefaultLocalizedContentAdmin();


            app.UseEndpoints(endpoints =>
            {
                /*
                endpoints.MapControllerRoute(
                    name: "OverrideFieldAdmin",
                    pattern: "Admin/OverrideField/{groupKey?}/{parentKey?}",
                    defaults: new { Controller = "OverrideFieldAdmin", Action = "Index" },
                    constraints: new { Controller = "Admin", Action = "Index" });
                */
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }

        protected void AddMockedUsers()
        {
            MockUserStore<TUser>.AddMockedUser(
                new TUser(new
                {
                    Id = 1,
                    UserName = "testuser@draxjinn.info",
                    NormalizedUserName = "testuser@draxjinn.info",
                    Email = "testuser@draxjinn.info",
                    NormalizedEmail = "testuser@draxjinn.info",
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new[]
                {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "LocalizedContentAccess",
                        ClaimValue = "Read",
                    }
                });

            MockUserStore<TUser>.AddMockedUser(
                new TUser(new
                {
                    Id = 2,
                    UserName = "testadmin@draxjinn.info",
                    NormalizedUserName = "testadmin@draxjinn.info",
                    Email = "testadmin@draxjinn.info",
                    NormalizedEmail = "testadmin@draxjinn.info",
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new[]
                {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "ContentOverrideAccess",
                        ClaimValue = "Read",
                    }
                });

            MockUserStore<TUser>.AddMockedUser(
                new TUser(new
                {
                    Id = 3,
                    UserName = "testsuper@draxjinn.info",
                    NormalizedUserName = "testsuper@draxjinn.info",
                    Email = "testsuper@draxjinn.info",
                    NormalizedEmail = "testsuper@draxjinn.info",
                    PasswordHash = "test",
                    EmailConfirmed = true
                }),
                new[]
                {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "LocalizedContentAccess",
                        ClaimValue = "Read",
                    },
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "AdminAccess",
                        ClaimValue = "Standard20",
                    }
                });

        }
    }
}