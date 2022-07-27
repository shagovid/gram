using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    private static readonly OpenApiSecurityScheme SecurityScheme = new()
    {
        Name = "Bearer",
        Description = "Please enter your JWT token (without Bearer-prefix)",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http,
        In = ParameterLocation.Header
    };

    private static readonly OpenApiSecurityRequirement SecurityRequirement = new()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = SecurityScheme.Name
                }
            }, new List<string>()
        }
    };

    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{nameof(Rossgram)}.{nameof(WebApi)}", Version = "v1" });
                
            c.AddSecurityDefinition("Bearer", SecurityScheme);
            c.OperationFilter<AuthorizeCheckOperationFilter>();
            c.CustomSchemaIds(ExtractShortSchemaId);
        });
        return services;
    }

    private static string ExtractShortSchemaId(Type type)
    {
        // Try convert this:
        // Rossgram.Application.Auth.Commands.SignUp.SignUpResponseDTO.AccountDTO
        // into this:
        // SignUpResponse.Account
        string schemaId = type.FullName ?? "No fullname for class";
        schemaId = schemaId.Replace("+", ".");
        schemaId = schemaId.Replace("DTO", "");
                        
        Match match = Regex.Match(schemaId, ".+(?:Commands|Queries)\\.[^.]+\\.(?<short_name>.+)");
        return match.Success ? match.Groups["short_name"].Value : schemaId;
    }

    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{nameof(Rossgram)}.{nameof(WebApi)} v1"));
        return app;
    }

    private class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Security.Add(SecurityRequirement);
        }
    }
}