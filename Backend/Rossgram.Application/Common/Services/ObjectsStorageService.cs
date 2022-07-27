using System;
using System.IO;
using System.Threading.Tasks;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.Services;

public class ObjectsStorageService
{
    private readonly IObjectsStorageConfig _config;
    private readonly IObjectsStorageProvider _provider;
    private readonly MediaClassificationService _classificationService;
    private readonly ITimeService _time;
    private readonly IRandomService _random;

    public ObjectsStorageService(
        IObjectsStorageConfig config,
        IObjectsStorageProvider provider,
        MediaClassificationService classificationService,
        ITimeService time,
        IRandomService random)
    {
        _config = config;
        _provider = provider;
        _classificationService = classificationService;
        _time = time;
        _random = random;
    }

    private string GetUniquePathFor(string fileFullName)
    {
        FileType fileType = _classificationService.RecognizeExtension(Path.GetExtension(fileFullName));
        
        string folder;
        if (fileType == FileType.Photo) folder = _config.ImagesFolder;
        else if (fileType == FileType.Video) folder = _config.VideosFolder;
        else if (fileType == FileType.Audio) folder = _config.AudioFolder;
        else if (fileType == FileType.Document) folder = _config.DocumentsFolder;
        else if (fileType == FileType.Other) folder = _config.FilesFolder;
        else throw new NotImplementedException();
            
        DateTimeOffset now = _time.Now;
            
        string year = now.Year.ToString();
        string month = now.Month.ToString();
        string day = now.Day.ToString();

        long currentTime = now.Ticks;
        string randomString = _random.NextString();
        string uniqueValue = $"{currentTime}{randomString}";

        string filePath = $"{folder}/{year}/{month}/{day}/{uniqueValue}/{fileFullName}";
        return filePath;
    }
        
    public async Task<string> Upload(string fileFullName, Stream fileStream)
    {
        string key = GetUniquePathFor(fileFullName);
        await _provider.UploadFileAsync(key, fileStream);
        return key;
    }

    public string GetLinkFor(string objectsStorageKey)
    {
        return _provider.GetLinkFor(objectsStorageKey, _config.LinkExpirationTime);
    }
}