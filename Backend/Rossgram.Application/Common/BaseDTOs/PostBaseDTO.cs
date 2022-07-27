using System;
using System.Collections.Generic;

namespace Rossgram.Application.Common.BaseDTOs;

public class PostBaseDTO : BaseDTO
{
    public AccountShortBaseDTO? Owner { get; } 
    public DateTimeOffset CreatedAt { get; }
    public List<AttachmentBaseDTO> Attachments { get; } 
    public string Comment { get; }
    public int LikesCount { get; }
    public bool IsLiked { get; }
    public int CommentsCount { get; }
    public bool IsFavorite { get; }

    public PostBaseDTO(
        long id, 
        DateTimeOffset createdAt, 
        List<AttachmentBaseDTO> attachments, 
        string comment, 
        int likesCount, 
        bool isLiked, 
        int commentsCount, 
        bool isFavorite,
        AccountShortBaseDTO? owner = null) 
        : base(id)
    {
        Owner = owner;
        CreatedAt = createdAt;
        Attachments = attachments;
        Comment = comment;
        LikesCount = likesCount;
        IsLiked = isLiked;
        CommentsCount = commentsCount;
        IsFavorite = isFavorite;
    }
}