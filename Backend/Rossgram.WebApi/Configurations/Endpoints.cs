using Microsoft.AspNetCore.Builder;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IApplicationBuilder UseConfiguredEndpoints(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        return app;
    }
}