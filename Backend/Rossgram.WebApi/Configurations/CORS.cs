using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IServiceCollection AddConfiguredCors(this IServiceCollection services)
    {
        services.AddCors(x =>
        {
            x.AddDefaultPolicy(policy => policy
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders(HeaderNames.ContentDisposition));
        });
        return services;
    }

    public static IApplicationBuilder UseConfiguredCors(this IApplicationBuilder app)
    {
        app.UseCors();
        return app;
    }
}