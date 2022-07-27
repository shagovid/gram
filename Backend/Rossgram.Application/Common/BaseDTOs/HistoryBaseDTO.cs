using System;

namespace Rossgram.Application.Common.BaseDTOs;

public class HistoryBaseDTO : BaseDTO
{
    public AccountShortBaseDTO? Owner { get; }
    public DateTimeOffset CreatedAt { get; }
    public UploadedFileBaseDTO File { get; }

    public HistoryBaseDTO(
        long id,  
        DateTimeOffset createdAt, 
        UploadedFileBaseDTO file,
        AccountShortBaseDTO? owner = null) 
        : base(id)
    {
        Owner = owner;
        CreatedAt = createdAt;
        File = file;
    }
}