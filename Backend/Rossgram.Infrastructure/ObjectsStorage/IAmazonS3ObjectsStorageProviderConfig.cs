using System;

namespace Rossgram.Infrastructure.ObjectsStorage;

public interface IAmazonS3ObjectsStorageProviderConfig
{
    public string ServiceURL { get; }
    public string BucketName { get; }
    public string AccessKey { get; }
    public string SecretKey { get; }
}