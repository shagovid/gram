using System.Collections.Generic;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class GroupConversation : Conversation
{
    public string Name { get; set; }
    public ICollection<GroupConversationMember> Members { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected GroupConversation(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public GroupConversation(
        long id, 
        string name, 
        ICollection<GroupConversationMember> members,
        ICollection<Message> messages) 
        : base(id, ConversationType.Group, messages)
    {
        Name = name;
        Members = members;
    }
}