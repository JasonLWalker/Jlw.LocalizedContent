﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Jlw.Data.LocalizedContent;
using Jlw.Web.Rcl.LocalizedContent;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizedContentAdminExtensions
    {
        internal class LocalizedContentAdminConfigureOptions : IPostConfigureOptions<StaticFileOptions>
        {
            private readonly IWebHostEnvironment _environment;

            public LocalizedContentAdminConfigureOptions(IWebHostEnvironment environment)
            {
                _environment = environment;
            }

            public void PostConfigure(string name, StaticFileOptions options)
            {
                // Basic initialization in case the options weren't initialized by any other component
                options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();
                if (options.FileProvider == null && _environment.WebRootFileProvider == null)
                {
                    throw new InvalidOperationException("Missing FileProvider.");
                }
                options.FileProvider ??= _environment.WebRootFileProvider;
                // Add our provider
                var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, "resources");
                options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
            }
        }

        public static IServiceCollection AddLocalizedContentAdmin(this IServiceCollection services, Action<LocalizedContentFieldRepositoryOptions> options = null)
        {
            

            services.ConfigureOptions(typeof(LocalizedContentAdminConfigureOptions));
            return services;
        }



        public static AuthorizationOptions AddDefaultLocalizedContentAdminAuthorizationPolicy(this AuthorizationOptions options)
        {
            options.AddPolicy("LocalizedContentUser", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.Claims.Any(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentCreate", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Create", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentRead", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Read", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentUpdate", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Update", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            options.AddPolicy("LocalizedContentDelete", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim(claim => claim.Type.Equals("LocalizedContentAccess", StringComparison.InvariantCultureIgnoreCase) && claim.Value.Equals("Delete", StringComparison.InvariantCultureIgnoreCase));
                });
            });

            return options;
        }

        public static IApplicationBuilder UseDefaultLocalizedContentAdmin(this IApplicationBuilder app)
        {
            return app.UseEndpoints(endpoints =>
            {
                // Localized Content Field Admin mapping
                endpoints.MapControllerRoute(
                    name: "LocalizedContentFieldAdmin",
                    pattern: "Admin/LocalizedContentField/{groupKey?}/{parentKey?}",
                    defaults: new {Controller = "Admin", Action="Index", Area= "LocalizedContentField"},
                    constraints: new { Controller = "Admin", Action = "Index", Area = "LocalizedContentField" });
                endpoints.MapControllerRoute(
                    name: "LocalizedContentFieldWizardAdmin",
                    pattern: "Admin/LocalizedContentWizard/{groupKey?}/{parentKey?}",
                    defaults: new { Controller = "Admin", Action = "Wizard", Area = "LocalizedContentField" },
                    constraints: new { Controller = "Admin", Action = "Wizard", Area = "LocalizedContentField" });
                endpoints.MapControllerRoute(
                    name: "LocalizedContentFieldEmailAdmin",
                    pattern: "Admin/LocalizedContentEmail/{groupKey?}/{parentKey?}",
                    defaults: new { Controller = "Admin", Action = "Email", Area = "LocalizedContentField" },
                    constraints: new { Controller = "Admin", Action = "Email", Area = "LocalizedContentField" });



                // Localized Content Text Admin mapping
                endpoints.MapControllerRoute(
                    name: "LocalizedContentTextAdmin",
                    pattern: "Admin/LocalizedContentText/{groupKey?}/{fieldKey?}/{language?}",
                    defaults: new { Controller = "Admin", Action = "Index", Area = "LocalizedContentText" },
                    constraints: new { Controller = "Admin", Action = "Index", Area = "LocalizedContentText" });

                endpoints.MapControllerRoute(
                    name: "LocalizedGroupDataItemAdmin",
                    pattern: "Admin/LocalizedGroupDataItem",
                    defaults: new { Controller = "Admin", Action = "Index", Area = "LocalizedGroupDataItem" },
                    constraints: new { Controller = "Admin", Action = "Index", Area = "LocalizedGroupDataItem" });

            });
        }
    }
}