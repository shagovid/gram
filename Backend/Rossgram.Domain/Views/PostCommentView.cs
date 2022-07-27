using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Views;

public class PostCommentView
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; } = null!;
    public long PostId { get; set; }
    public Post Post { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string Text { get; set; } = null!;
    public int LikesCount { get; set; }
    public ICollection<PostCommentLike> Likes { get; set; } = null!;
}