

using Microsoft.AspNetCore.Builder;
using Rossgram.WebApi.Middlewares;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }
}