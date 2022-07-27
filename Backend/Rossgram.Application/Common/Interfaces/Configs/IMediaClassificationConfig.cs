namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IMediaClassificationConfig
{
    public string[] ImagesExtensions { get; }
    public string[] VideosExtensions { get; }
    public string[] AudioExtensions { get; }
    public string[] DocumentsExtensions { get; }
}