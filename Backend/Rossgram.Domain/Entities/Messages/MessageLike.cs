namespace Rossgram.Domain.Entities.Messages;

public class MessageLike : Entity
{
    public long MessageId { get; set; }
    public Message Message { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected MessageLike(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessageLike(
        long id, 
        long messageId, 
        Message message, 
        long ownerId, 
        Account owner) 
        : base(id)
    {
        MessageId = messageId;
        Message = message;
        OwnerId = ownerId;
        Owner = owner;
    }
}