using System.IO;
using Microsoft.AspNetCore.Http;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Application.Common.Services;
using Rossgram.Domain;

namespace Rossgram.WebApi.Services;

public class FormFileAdapter
{
    private readonly MediaClassificationService _mediaClassification;

    public FormFileAdapter(
        MediaClassificationService mediaClassification)
    {
        _mediaClassification = mediaClassification;
    }

    public FileData ToFileData(IFormFile formFile)
    {
        string name = Path.GetFileNameWithoutExtension(formFile.FileName);
        string extension = Path.GetExtension(formFile.FileName);
        
        return new FileData()
        {
            Name = name,
            Extension = extension,
            Type = _mediaClassification.RecognizeExtension(extension),
            Stream = formFile.OpenReadStream()
        };
    }
}