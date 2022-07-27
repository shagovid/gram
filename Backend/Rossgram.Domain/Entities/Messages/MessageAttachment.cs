using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public abstract class MessageAttachment : Entity
{
    public long MessageId { get; set; }
    public Message Message { get; set; }
    public int Order { get; set; }
    public AttachmentType Type { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected MessageAttachment(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessageAttachment(
        long id, 
        long messageId, 
        Message message,
        int order, 
        AttachmentType type) 
        : base(id)
    {
        MessageId = messageId;
        Message = message;
        Order = order;
        Type = type;
    }
}