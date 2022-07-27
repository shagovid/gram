using System;
using System.Collections.Generic;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Entities.Posts;

public class PostComment : Entity
{
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
    public long PostId { get; set; }
    public Post Post { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string Text { get; set; }
    public ICollection<PostCommentLike> Likes { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected PostComment(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostComment(
        long id, 
        long ownerId,
        Account owner,
        long postId, 
        Post post, 
        DateTimeOffset timeStamp,
        string text, 
        ICollection<PostCommentLike> likes) 
        : base(id)
    {
        OwnerId = ownerId;
        Owner = owner;
        PostId = postId;
        Post = post;
        TimeStamp = timeStamp;
        Text = text;
        Likes = likes;
    }
}