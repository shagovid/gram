using System;
using System.Text.RegularExpressions;

namespace Rossgram.WebApi.Services;

public class NoSecretsService
{
    public int UsernameMinLength { get; } = 3;
    public char[] UsernameAvailableSymbols { get; } = {'.', '_'};
    public int PasswordMinLength { get; } = 8;
    public Regex EmailPattern { get; } = new(".+@.+\\..+");
    public string[] MediaClassificationImagesExtensions { get; } = new [] {".jpg", ".jpeg", ".png"};
    public string[] MediaClassificationVideosExtensions { get; } = new [] {".mp4"};
    public string[] MediaClassificationAudioExtensions { get; } = new [] {".ogg", ".mp3"};
    public string[] MediaClassificationDocumentsExtensions { get; } = new [] {".doc", ".docx"};
    public string ObjectsStorageImageFolder { get; } = "images";
    public string ObjectsStorageVideosFolder { get; } = "videos";
    public string ObjectsStorageAudioFolder { get; } = "audio";
    public string ObjectsStorageDocumentsFolder { get; } = "docs";
    public string ObjectsStorageFilesFolder { get; } = "files";
    public TimeSpan ObjectsStorageLinkExpirationTime { get; } = TimeSpan.FromMinutes(5);
    public int CommentTextMaxLength { get; } = 200;
    public int PostMinMediaCount { get; } = 1;
    public int PostMaxMediaCount { get; } = 10;
    public TimeSpan HistoryTimeToArchive { get; } = TimeSpan.FromHours(24);
    public int HistoryMaxCountPerAccount { get; } = 20;
}