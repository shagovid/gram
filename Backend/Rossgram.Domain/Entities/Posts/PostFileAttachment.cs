using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Posts;

public class PostAttachmentFile : PostAttachment
{
    public long UploadedFileId { get; set; }
    public UploadedFile UploadedFile { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected PostAttachmentFile(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostAttachmentFile(
        long id, 
        long postId,
        Post post, 
        int order,
        long uploadedFileId,
        UploadedFile uploadedFile) 
        : base(id, postId, post, order, AttachmentType.File)
    {
        UploadedFileId = uploadedFileId;
        UploadedFile = uploadedFile;
    }
}