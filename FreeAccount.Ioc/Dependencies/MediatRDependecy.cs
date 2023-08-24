using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FreeAccount.Ioc.Behavior;
using System;
using System.Reflection;

namespace FreeAccount.Ioc.Dependencies
{
    public static class MediatRDependecy
    {
        public static IServiceCollection AddMediatRDependency(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load(Assembly.GetEntryAssembly().GetName().Name.Replace(".Api", ".Domain"));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
