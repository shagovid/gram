using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class MessageLinkAttachment : MessageAttachment
{
    public string Link { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected MessageLinkAttachment(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessageLinkAttachment(
        long id, 
        long messageId,
        Message message, 
        int order,
        string link) 
        : base(id, messageId, message, order, AttachmentType.Link)
    {
        Link = link;
    }
}