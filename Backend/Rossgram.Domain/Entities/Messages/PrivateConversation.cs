using System.Collections.Generic;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class PrivateConversation : Conversation
{
    public long OlderAccountId { get; set; }
    public Account OlderAccount { get; set; }
    public long NewerAccountId { get; set; }
    public Account NewerAccount { get; set; }

    #region EF Core constructor
#pragma warning disable 8618
    protected PrivateConversation(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public PrivateConversation(
        long id, 
        long olderAccountId, 
        Account olderAccount, 
        long newerAccountId, 
        Account newerAccount,
        ICollection<Message> messages) 
        : base(id, ConversationType.Private, messages)
    {
        OlderAccountId = olderAccountId;
        OlderAccount = olderAccount;
        NewerAccountId = newerAccountId;
        NewerAccount = newerAccount;
    }
}