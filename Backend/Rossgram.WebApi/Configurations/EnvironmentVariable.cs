using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Rossgram.Domain.Enumerations;

namespace Rossgram.WebApi.Configurations;

public class EnvironmentVariable : Enumeration
{
    public static readonly EnvironmentVariable DatabaseHost = new(
        id: 1,
        code: "DATABASE_HOST",
        defaultValue: "localhost");
    public static readonly EnvironmentVariable DatabasePort = new(
        id: 2,
        code: "DATABASE_PORT",
        defaultValue: "5432");

    public static readonly EnvironmentVariable DatabaseName = new(
        id: 3,
        code: "DATABASE_NAME",
        defaultValue: "database");

    public static readonly EnvironmentVariable DatabaseUser = new(
        id: 4,
        code: "DATABASE_USER",
        defaultValue: "user");

    public static readonly EnvironmentVariable DatabasePassword = new(
        id: 5,
        code: "DATABASE_PASSWORD",
        defaultValue: "password");

    public static readonly EnvironmentVariable<int> KestrelPort = new(
        id: 6,
        code: "KESTREL_PORT",
        convertFunc: int.Parse,
        defaultValue: "5000");

    public static readonly EnvironmentVariable JwtIssuer = new(
        id: 7,
        code: "JWT_ISSUER");

    public static readonly EnvironmentVariable JwtAudience = new(
        id: 8,
        code: "JWT_AUDIENCE");

    public static readonly EnvironmentVariable<SymmetricSecurityKey> JwtSigningKey = new(
        id: 9,
        code: "JWT_SIGNING_KEY",
        convertFunc: key =>
        {
            byte[] bytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref bytes, 128);
            return new SymmetricSecurityKey(bytes);
        });

    public static readonly EnvironmentVariable<TimeSpan> JwtLifetime = new(
        id: 10,
        code: "JWT_LIFETIME",
        convertFunc: x => TimeSpan.FromHours(int.Parse(x)));

    public static readonly EnvironmentVariable AmazonS3ServiceURL = new(
        id: 11,
        code: "AMAZON_S3_SERVICE_URL");
        
    public static readonly EnvironmentVariable AmazonS3BucketName = new(
        id: 12,
        code: "AMAZON_S3_BUCKET_NAME");

    public static readonly EnvironmentVariable AmazonS3AccessKey = new(
        id: 13,
        code: "AMAZON_S3_ACCESS_KEY");
        
    public static readonly EnvironmentVariable AmazonS3SecretKey = new(
        id: 14,
        code: "AMAZON_S3_SECRET_KEY");

    public bool IsDefault => Environment.GetEnvironmentVariable(this.Code) == null;
    public string Value => (Environment.GetEnvironmentVariable(this.Code) ?? this.DefaultValue) ??
                           throw new InvalidOperationException($"Cannot find {Code} environment variable");
    public string? DefaultValue { get; private set; }

    public EnvironmentVariable(
        int id,
        string code,
        string? defaultValue = null)
        : base(id, code)
    {
        this.DefaultValue = defaultValue;
    }

    public static implicit operator string(EnvironmentVariable variable)
    {
        return variable.ToString();
    }

    public override string ToString()
    {
        return this.Value;
    }
}

public class EnvironmentVariable<T> : EnvironmentVariable
{
    public T ConvertedValue => this._convertFunc(this.Value);
    private readonly Func<string, T> _convertFunc;

    public EnvironmentVariable(
        int id,
        string code,
        Func<string, T> convertFunc,
        string defaultValue = "null")
        : base(id, code, defaultValue)
    {
        this._convertFunc = convertFunc;
    }

    public static implicit operator T(EnvironmentVariable<T> variable)
    {
        return variable.ConvertedValue;
    }
}