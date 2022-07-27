using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Posts;

public abstract class PostAttachment : Entity
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    public int Order { get; set; }
    public AttachmentType AttachmentType { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected PostAttachment(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostAttachment(
        long id, 
        long postId,
        Post post, 
        int order, 
        AttachmentType attachmentType) 
        : base(id)
    {
        PostId = postId;
        Post = post;
        Order = order;
        AttachmentType = attachmentType;
    }
}