using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class MessageFileAttachment : MessageAttachment
{
    public long UploadedFileId { get; set; }
    public UploadedFile UploadedFile { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected MessageFileAttachment(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessageFileAttachment(
        long id, 
        long messageId,
        Message message, 
        int order,
        long uploadedFileId,
        UploadedFile uploadedFile) 
        : base(id, messageId, message, order, AttachmentType.File)
    {
        UploadedFileId = uploadedFileId;
        UploadedFile = uploadedFile;
    }
}