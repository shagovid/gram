using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.BaseDTOs;

public class UploadedFileBaseDTO : BaseDTO
{
    public FileType Type { get; }
    public string FullName { get; }
    public string Link { get; }
    public UploadedFileBaseDTO(
        long id, 
        FileType type,
        string fullName,
        string link) 
        : base(id)
    {
        Type = type;
        FullName = fullName;
        Link = link;
    }
}