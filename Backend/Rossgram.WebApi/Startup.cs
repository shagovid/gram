using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.Domain.ValueObjects;
using Rossgram.Infrastructure.ObjectsStorage;
using Rossgram.WebApi.Configurations;
using Rossgram.WebApi.Services;

namespace Rossgram.WebApi;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        // Register configs
        services.AddSingleton<SecretsService>();
        services.AddSingleton<NoSecretsService>();
        services.AddSingleton<ConfigsService>();
        services.AddSingleton<IDatabaseConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IAuthConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IMediaClassificationConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IObjectsStorageConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IAmazonS3ObjectsStorageProviderConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IUsernameConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IPasswordConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IEmailConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<ICommentConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IHistoriesConfig>(x => x.GetRequiredService<ConfigsService>());
        services.AddSingleton<IPostConfig>(x => x.GetRequiredService<ConfigsService>());
            
        // Register static-class services
        services.AddTransient<ITimeService, TimeService>();
        services.AddTransient<IRandomService, RandomService>();
            
        // Register services
        services.AddScoped<MediaClassificationService>();
        services.AddScoped<FormFileAdapter>();
        services.AddScoped<ObjectsStorageService>();
        services.AddScoped<IObjectsStorageProvider, AmazonS3ObjectsStorageProvider>();
        services.AddScoped<NicknameController>();
        services.AddScoped<PasswordController>();
        services.AddScoped<EmailController>();
        services.AddScoped<CommentController>();
            
        services.AddConfiguredApplication();
        services.AddConfiguredCors();
        services.AddConfiguredControllers();
        services.AddConfiguredDatabase();
        services.AddConfiguredAuthentication();
#if DEBUG
        services.AddConfiguredSwagger();
#endif
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
#if DEBUG
        app.UseDeveloperExceptionPage();
        app.UseConfiguredSwagger();
#endif
        app.UseRouting();
        app.UseConfiguredCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseLoggerMiddleware();
        app.UseExceptionHandlerMiddleware();
        app.UseConfiguredEndpoints();
    }
}