using System;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.ValueObjects;
using Rossgram.Infrastructure.ObjectsStorage;

namespace Rossgram.WebApi.Services;

public class ConfigsService :
    IDatabaseConfig,
    IAuthConfig,
    IMediaClassificationConfig,
    IObjectsStorageConfig,
    IAmazonS3ObjectsStorageProviderConfig,
    IUsernameConfig,
    IPasswordConfig,
    IEmailConfig,
    ICommentConfig,
    IHistoriesConfig,
    IPostConfig
{
    string IDatabaseConfig.ConnectionString => _secret.DatabaseConnectionString;

    string IAuthConfig.JwtIssuer => _secret.JwtIssuer;
    string IAuthConfig.JwtAudience => _secret.JwtAudience;
    TimeSpan IAuthConfig.JwtLifetime => _secret.JwtLifetime;
    SecurityKey IAuthConfig.JwtSigningKey => _secret.JwtSigningKey;
        
    string[] IMediaClassificationConfig.ImagesExtensions => _noSecret.MediaClassificationImagesExtensions;
    string[] IMediaClassificationConfig.VideosExtensions => _noSecret.MediaClassificationVideosExtensions;
    string[] IMediaClassificationConfig.AudioExtensions => _noSecret.MediaClassificationAudioExtensions;
    string[] IMediaClassificationConfig.DocumentsExtensions => _noSecret.MediaClassificationDocumentsExtensions;
    
    string IObjectsStorageConfig.ImagesFolder => _noSecret.ObjectsStorageImageFolder;
    string IObjectsStorageConfig.VideosFolder => _noSecret.ObjectsStorageVideosFolder;
    string IObjectsStorageConfig.AudioFolder => _noSecret.ObjectsStorageAudioFolder;
    string IObjectsStorageConfig.DocumentsFolder => _noSecret.ObjectsStorageDocumentsFolder;
    string IObjectsStorageConfig.FilesFolder => _noSecret.ObjectsStorageFilesFolder;
    TimeSpan IObjectsStorageConfig.LinkExpirationTime => _noSecret.ObjectsStorageLinkExpirationTime;
        
    string IAmazonS3ObjectsStorageProviderConfig.ServiceURL => _secret.AmazonS3ServiceURL;
    string IAmazonS3ObjectsStorageProviderConfig.BucketName => _secret.AmazonS3BucketName;
    string IAmazonS3ObjectsStorageProviderConfig.AccessKey => _secret.AmazonS3AccessKey;
    string IAmazonS3ObjectsStorageProviderConfig.SecretKey => _secret.AmazonS3SecretKey;

    int IUsernameConfig.MinLength => _noSecret.UsernameMinLength;

    char[] IUsernameConfig.AvailableSymbols => _noSecret.UsernameAvailableSymbols;
        
    int IPasswordConfig.MinLength => _noSecret.PasswordMinLength;
        
    Regex IEmailConfig.Pattern => _noSecret.EmailPattern;
    
    int ICommentConfig.TextMaxLength => _noSecret.CommentTextMaxLength;
    
    TimeSpan IHistoriesConfig.TimeToArchive => _noSecret.HistoryTimeToArchive;
    int IHistoriesConfig.MaxCountPerAccount => _noSecret.HistoryMaxCountPerAccount;
    
    int IPostConfig.MinAttachmentsCount => _noSecret.PostMinMediaCount;
    int IPostConfig.MaxAttachmentsCount => _noSecret.PostMaxMediaCount;
        
    private readonly NoSecretsService _noSecret;
    private readonly SecretsService _secret;

    public ConfigsService(
        NoSecretsService noSecretsService,
        SecretsService secretsService)
    {
        _noSecret = noSecretsService;
        _secret = secretsService;
    }
}