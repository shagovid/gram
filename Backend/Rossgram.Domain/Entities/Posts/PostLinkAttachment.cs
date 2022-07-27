using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Posts;

public class PostAttachmentLink : PostAttachment
{
    public string Link { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected PostAttachmentLink(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PostAttachmentLink(
        long id, 
        long postId,
        Post post, 
        int order,
        string link) 
        : base(id, postId, post, order, AttachmentType.Link)
    {
        Link = link;
    }
}