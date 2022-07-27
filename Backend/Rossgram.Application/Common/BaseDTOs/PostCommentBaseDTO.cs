using System;

namespace Rossgram.Application.Common.BaseDTOs;

public class PostCommentBaseDTO : BaseDTO
{
    public AccountShortBaseDTO? Owner { get; } 
    public DateTimeOffset CreatedAt { get; }
    public string Text { get; }
    public int LikesCount { get; }
    public bool IsLiked { get; }

    public PostCommentBaseDTO(
        long id,  
        DateTimeOffset createdAt, 
        string text, 
        int likesCount, 
        bool isLiked,
        AccountShortBaseDTO? owner = null) 
        : base(id)
    {
        Owner = owner;
        CreatedAt = createdAt;
        Text = text;
        LikesCount = likesCount;
        IsLiked = isLiked;
    }
}