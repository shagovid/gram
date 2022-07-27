using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rossgram.Application.Common.Behaviours;
using Rossgram.Domain;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static IServiceCollection AddConfiguredApplication(this IServiceCollection services)
    {
        // Import services for Rossgram.Application
        // Import pipeline behaviors for Rossgram.Application
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

        Assembly applicationAssembly =
            AppDomain.CurrentDomain.GetAssembly($"{nameof(Rossgram)}.{nameof(Application)}");

        services.AddMediatR(applicationAssembly);

        return services;
    }
}