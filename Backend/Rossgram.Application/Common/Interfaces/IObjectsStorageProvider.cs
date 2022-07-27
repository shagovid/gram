using System;
using System.IO;
using System.Threading.Tasks;

namespace Rossgram.Application.Common.Interfaces;

public interface IObjectsStorageProvider
{
    public Task UploadFileAsync(string fullFileName, Stream fileStream);
    public string GetLinkFor(string key, TimeSpan expirationTime);
}