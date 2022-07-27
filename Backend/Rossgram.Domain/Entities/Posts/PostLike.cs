namespace Rossgram.Domain.Entities.Posts;

public class PostLike : Entity
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected PostLike(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostLike(
        long id, 
        long postId, 
        Post post, 
        long ownerId, 
        Account owner) 
        : base(id)
    {
        PostId = postId;
        Post = post;
        OwnerId = ownerId;
        Owner = owner;
    }
}