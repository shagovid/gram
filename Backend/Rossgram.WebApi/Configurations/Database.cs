using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Infrastructure;
using Rossgram.Infrastructure.Database;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services)
    {
        services.AddDbContext<IAppDbContext, AppDbContext>((provider, options) =>
        {
            IDatabaseConfig databaseConfig = provider.GetRequiredService<IDatabaseConfig>();
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(
                connectionString: databaseConfig.ConnectionString,
                npgsqlOptionsAction: builder =>
                {
                    builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
        });
        return services;
    }
}