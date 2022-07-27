using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.BaseDTOs;

public abstract class AttachmentBaseDTO : BaseDTO
{
    public int Order { get; }
    public AttachmentType AttachmentType { get; }

    protected AttachmentBaseDTO(
        long id, 
        int order,
        AttachmentType attachmentType) : base(id)
    {
        Order = order;
        AttachmentType = attachmentType;
    }
}

public class FileAttachmentBaseDTO : AttachmentBaseDTO
{
    public UploadedFileBaseDTO? File { get; }
    
    public FileAttachmentBaseDTO(
        long id, 
        int order, 
        UploadedFileBaseDTO? file) 
        : base(id, order, AttachmentType.File)
    {
        File = file;
    }
}

public class LinkAttachmentBaseDTO : AttachmentBaseDTO
{
    public string Link { get; }
    
    public LinkAttachmentBaseDTO(
        long id, 
        int order, 
        string link) 
        : base(id, order, AttachmentType.Link)
    {
        Link = link;
    }
}

public class PostAttachmentBaseDTO : AttachmentBaseDTO
{
    public PostBaseDTO? Post { get; }
    
    public PostAttachmentBaseDTO(
        long id, 
        int order, 
        PostBaseDTO? post) 
        : base(id, order, AttachmentType.Post)
    {
        Post = post;
    }
}

public class HistoryAttachmentBaseDTO : AttachmentBaseDTO
{
    public HistoryBaseDTO? History { get; }
    
    public HistoryAttachmentBaseDTO(
        long id, 
        int order, 
        HistoryBaseDTO? history) 
        : base(id, order, AttachmentType.Post)
    {
        History = history;
    }
}