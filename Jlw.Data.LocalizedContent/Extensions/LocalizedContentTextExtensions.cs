// ***********************************************************************
// Assembly         : Jlw.Data.LocalizedContent
// Author           : jlwalker
// Created          : 05-20-2021
//
// Last Modified By : jlwalker
// Last Modified On : 06-15-2021
// ***********************************************************************
// <copyright file="LocalizedContentTextExtensions.cs" company="Jason L. Walker">
//     Copyright �2012-2021 Jason L. Walker
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Jlw.Data.LocalizedContent;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.Extensions.Options;
using TOptions = Jlw.Data.LocalizedContent.LocalizedContentTextRepositoryOptions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Class LocalizedContentTextExtensions.
    /// </summary>
    /// TODO Edit XML Comment Template for LocalizedContentTextExtensions
    public static class LocalizedContentTextExtensions 
    {
        /// <summary>
        /// Adds the <see cref="LocalizedContentTextRepository">LocalizedContentTextRepository</see> to the service collection as a singleton instance.
        /// </summary>
        /// <param name="services">Service collection instance that this extension will act upon</param>
        /// <param name="setupAction">The setup action options used to initialize the repository singleton.</param>
        /// <returns>Returns the <paramref name="services">services</paramref> service collection to allow for method chaining.<br /></returns>
        public static IServiceCollection AddLocalizedContentTextRepository(this IServiceCollection services, Action<TOptions> setupAction = null) 
        {
            if (setupAction != null)
                services.Configure(setupAction);

            services.AddSingleton<ILocalizedContentTextRepository>(provider =>
            {
                var options = provider.GetService<IOptions<TOptions>>() ?? new OptionsWrapper<TOptions>(provider.GetRequiredService<TOptions>());

                var dbClient = options?.Value?.DbClient ?? provider.GetRequiredService<IModularDbClient>();
                var connString = options?.Value?.ConnectionString ?? "";
                return new LocalizedContentTextRepository(dbClient, connString);
            });

            return services;
        }
    } 
} 
