using System;

namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IObjectsStorageConfig
{
    public string ImagesFolder { get; }
    public string VideosFolder { get; }
    public string AudioFolder { get; }
    public string DocumentsFolder { get; }
    public string FilesFolder { get; }
    public TimeSpan LinkExpirationTime { get; }
}