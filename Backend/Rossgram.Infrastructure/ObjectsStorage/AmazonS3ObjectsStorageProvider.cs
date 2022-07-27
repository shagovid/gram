using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;

namespace Rossgram.Infrastructure.ObjectsStorage;

public class AmazonS3ObjectsStorageProvider : IObjectsStorageProvider
{
    private readonly ITimeService _time;
    private readonly IAmazonS3ObjectsStorageProviderConfig _config;
    private readonly AmazonS3Client _s3Client;

    public AmazonS3ObjectsStorageProvider(
        ITimeService time,
        IAmazonS3ObjectsStorageProviderConfig config)
    {
        _time = time;
        _config = config;
        _s3Client = new AmazonS3Client(
            credentials: new BasicAWSCredentials(
                accessKey: _config.AccessKey,
                secretKey: _config.SecretKey
            ),
            clientConfig: new AmazonS3Config()
            {
                ServiceURL = _config.ServiceURL,
            }
        );
    }
        
    public async Task UploadFileAsync(string key, Stream fileStream)
    {
        PutObjectResponse response = await _s3Client.PutObjectAsync(new PutObjectRequest()
            {
                BucketName = _config.BucketName,
                Key = key,
                InputStream = fileStream,
                
            }
        );

        if (response.HttpStatusCode != HttpStatusCode.OK)
            throw new NotImplementedException();
    }

    public string GetLinkFor(string objectStorageKey, TimeSpan expirationTime)
    {
        return _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = _config.BucketName, 
                Key = objectStorageKey,
                Expires = _time.Now.Add(expirationTime).UtcDateTime,
            }
        );
    }
}