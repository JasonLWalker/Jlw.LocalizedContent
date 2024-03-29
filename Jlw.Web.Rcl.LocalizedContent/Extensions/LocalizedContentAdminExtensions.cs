﻿using System;
using System.Linq;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Jlw.Web.Rcl.LocalizedContent;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class LocalizedContentAdminExtensions
    {
        public const string AreaName = "LocalizedContent";

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

        public static IServiceCollection AddLocalizedContentAdmin(this IServiceCollection services, Action<IModularDbOptions> options = null)
        {

            services.TryAddSingleton<IWizardAdminSettings>(new WizardAdminSettings());
            services.AddLocalizedGroupDataItemRepository(options);
            services.AddLocalizedContentFieldRepository(options);
            services.AddLocalizedContentTextRepository(options);

            services.AddWizardFactoryRepository(options);
            services.TryAddSingleton<IWizardFactory>(provider =>
            {
                return new WizardFactory(provider.GetRequiredService<IWizardFactoryRepository>());
            });

            services.TryAddSingleton<ILanguageListModel>(provider =>
            {
                var repo = provider.GetRequiredService<ILocalizedGroupDataItemRepository>();
                return new LanguageListModel(repo);
            });

            services.TryAddSingleton<ICommonControlListModel>(provider =>
            {
                var repo = provider.GetRequiredService<ILocalizedGroupDataItemRepository>();
                return new CommonControlListModel(repo);
            });

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

            app.UseEndpoints(endpoints =>
            {
            });

            return app;
        }
    }
}
