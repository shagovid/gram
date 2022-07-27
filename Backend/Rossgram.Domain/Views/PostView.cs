using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Views;

public class PostView
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public ICollection<PostAttachment> Attachments { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public int LikesCount { get; set; }
    public ICollection<PostLike> Likes { get; set; } = null!;
    public int CommentsCount { get; set; }
    public ICollection<PostComment> Comments { get; set; } = null!;
    public ICollection<MessagePostAttachment> MessagesAttachments { get; set; } = null!;
}