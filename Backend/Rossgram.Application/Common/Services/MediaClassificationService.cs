using System.IO;
using System.Linq;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.Services;

public class MediaClassificationService
{
    private readonly IMediaClassificationConfig _config;

    public MediaClassificationService(
        IMediaClassificationConfig config)
    {
        _config = config;
    }

    public FileType RecognizeExtension(string extension)
    {
        if (_config.ImagesExtensions.Contains(extension)) return FileType.Photo;
        if (_config.VideosExtensions.Contains(extension)) return FileType.Video;
        if (_config.AudioExtensions.Contains(extension)) return FileType.Audio;
        if (_config.DocumentsExtensions.Contains(extension)) return FileType.Document;
        return FileType.Other;
    }
}