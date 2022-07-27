using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class MessagePostAttachment : MessageAttachment
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected MessagePostAttachment(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessagePostAttachment(
        long id, 
        long messageId,
        Message message, 
        int order,
        long postId,
        Post post) 
        : base(id, messageId, message, order, AttachmentType.Post)
    {
        PostId = postId;
        Post = post;
    }
}