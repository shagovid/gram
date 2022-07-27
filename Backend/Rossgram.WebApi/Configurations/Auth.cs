using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.WebApi.Services;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IServiceCollection AddConfiguredAuthentication(this IServiceCollection services)
    {
        // Current user auth
        services.AddHttpContextAccessor();
        services.AddTransient<ICurrentAuth, CurrentAuth>();
        services.AddScoped<AuthService>();

        // JWT auth settings
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IAuthConfig>((options, config) => 
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = config.JwtIssuer,

                    RequireAudience = true,
                    ValidateAudience = true,
                    ValidAudience = config.JwtAudience,

                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = true,
                    IssuerSigningKey = config.JwtSigningKey,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
                options.SaveToken = true;
            });
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        return services;
    }
}