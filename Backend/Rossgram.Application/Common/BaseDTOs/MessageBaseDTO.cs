using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Application.Common.BaseDTOs;

public class MessageBaseDTO : BaseDTO
{
    public AccountShortBaseDTO? Owner { get; }
    public DateTimeOffset TimeStamp { get; }
    public string Text { get; }
    public List<AttachmentBaseDTO> Attachments { get; }
    public bool? IsEdited { get; }
    public int? LikesCount { get; }
    public bool? IsLiked { get; }

    public MessageBaseDTO(
        long id, 
        DateTimeOffset timeStamp, 
        string text, 
        List<AttachmentBaseDTO> attachments,
        AccountShortBaseDTO? owner = null,
        bool? isEdited = null, 
        int? likesCount = null, 
        bool? isLiked = null) 
        : base(id)
    {
        Owner = owner;
        TimeStamp = timeStamp;
        Text = text;
        Attachments = attachments;
        IsEdited = isEdited;
        LikesCount = likesCount;
        IsLiked = isLiked;
    }
}