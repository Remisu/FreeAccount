using Microsoft.AspNetCore.Builder;
using FreeAccount.Ioc.Middleware;

namespace FreeAccount.Ioc.Dependencies
{
    public static class AppDependency
    {
        public static IApplicationBuilder UseAppDependency(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
