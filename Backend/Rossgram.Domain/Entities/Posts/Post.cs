using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Entities.Posts;

public class Post : Entity
{
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public ICollection<PostAttachment> Attachments { get; set; }
    public string Comment { get; set; }
    public ICollection<PostLike> Likes { get; set; }
    public ICollection<PostComment> Comments { get; set; }
    public ICollection<MessagePostAttachment> MessagesAttachments { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected Post(long  id) : base(id)
    {
    }
#pragma warning restore 8618
    #endregion

    public Post(
        long id, 
        long ownerId, 
        Account owner, 
        DateTimeOffset timeStamp,
        ICollection<PostAttachment> attachments, 
        string comment, 
        ICollection<PostLike> likes, 
        ICollection<PostComment> comments,
        ICollection<MessagePostAttachment> messagesAttachments) 
        : base(id)
    {
        OwnerId = ownerId;
        Owner = owner;
        TimeStamp = timeStamp;
        Attachments = attachments;
        Comment = comment;
        Likes = likes;
        Comments = comments;
        MessagesAttachments = messagesAttachments;
    }
}