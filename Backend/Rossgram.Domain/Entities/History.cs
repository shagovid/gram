using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Domain.Entities;

public class History : Entity
{
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long UploadedFileId { get; set; }
    public UploadedFile UploadedFile { get; set; }
    public ICollection<MessageHistoryAttachment> MessagesAttachments { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected History(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public History(
        long id, 
        long ownerId, 
        Account owner,
        DateTimeOffset createdAt, 
        long uploadedFileId, 
        UploadedFile uploadedFile,
        ICollection<MessageHistoryAttachment> messagesAttachments) 
        : base(id)
    {
        OwnerId = ownerId;
        Owner = owner;
        CreatedAt = createdAt;
        UploadedFileId = uploadedFileId;
        UploadedFile = uploadedFile;
        MessagesAttachments = messagesAttachments;
    }
}