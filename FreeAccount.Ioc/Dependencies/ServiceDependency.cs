using Microsoft.Extensions.DependencyInjection;
using FreeAccount.Ioc.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeAccount.Ioc.Dependencies
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
        }
    }
}
