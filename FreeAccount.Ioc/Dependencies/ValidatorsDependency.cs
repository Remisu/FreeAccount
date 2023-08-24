using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace FreeAccount.Ioc.Dependencies
{
    public static class ValidatorsDependency
    {
        public static IServiceCollection AddValidatorsDependency(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load(Assembly.GetEntryAssembly().GetName().Name.Replace(".Api", ".Domain"));

            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

            return services;
        }
    }
}
