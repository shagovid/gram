using System.Collections.Generic;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public abstract class Conversation : Entity
{
    public ConversationType ConversationType { get; set; }
    public ICollection<Message> Messages { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected Conversation(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    protected Conversation(
        long id, 
        ConversationType conversationType,
        ICollection<Message> messages) 
        : base(id)
    {
        ConversationType = conversationType;
        Messages = messages;
    }
}