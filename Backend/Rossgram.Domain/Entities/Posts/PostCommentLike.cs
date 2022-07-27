namespace Rossgram.Domain.Entities.Posts;

public class PostCommentLike : Entity
{
    public long CommentId { get; set; }
    public PostComment Comment { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected PostCommentLike(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostCommentLike(
        long id, 
        long commentId, 
        PostComment comment, 
        long ownerId, 
        Account owner) 
        : base(id)
    {
        CommentId = commentId;
        Comment = comment;
        OwnerId = ownerId;
        Owner = owner;
    }
}