using System;
using Microsoft.IdentityModel.Tokens;
using Rossgram.Application.Common.Interfaces;
using Rossgram.WebApi.Configurations;

namespace Rossgram.WebApi.Services;

public class SecretsService
{
    public string JwtIssuer => EnvironmentVariable.JwtIssuer;
    public string JwtAudience => EnvironmentVariable.JwtAudience;
    public TimeSpan JwtLifetime => EnvironmentVariable.JwtLifetime;
    public SecurityKey JwtSigningKey => EnvironmentVariable.JwtSigningKey;

    public string DatabaseConnectionString => $"Server={EnvironmentVariable.DatabaseHost};" +
                                              $"Port={EnvironmentVariable.DatabasePort};" +
                                              $"Database={EnvironmentVariable.DatabaseName};" +
                                              $"User Id={EnvironmentVariable.DatabaseUser};" +
                                              $"Password={EnvironmentVariable.DatabasePassword}";

    public string AmazonS3ServiceURL => EnvironmentVariable.AmazonS3ServiceURL;
    public string AmazonS3BucketName => EnvironmentVariable.AmazonS3BucketName;
    public string AmazonS3AccessKey => EnvironmentVariable.AmazonS3AccessKey;
    public string AmazonS3SecretKey => EnvironmentVariable.AmazonS3SecretKey;
}