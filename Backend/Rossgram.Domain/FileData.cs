using System.IO;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain;

public class FileData
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public string FullName => $"{Name}{Extension}";
    public FileType Type { get; set; }
    public Stream Stream { get; set; }
}