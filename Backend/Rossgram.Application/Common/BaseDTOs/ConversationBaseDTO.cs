using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.BaseDTOs;

public abstract class ConversationBaseDTO : BaseDTO
{
    public ConversationType ConversationType { get; }
    public MessageBaseDTO LastMessage { get; }

    protected ConversationBaseDTO(
        long id, 
        ConversationType conversationType, 
        MessageBaseDTO lastMessage) 
        : base(id)
    {
        ConversationType = conversationType;
        LastMessage = lastMessage;
    }
}

public class PrivateConversationBaseDTO : ConversationBaseDTO
{
    public AccountShortBaseDTO Recipient { get; }


    public PrivateConversationBaseDTO(
        long id, 
        AccountShortBaseDTO recipient, 
        MessageBaseDTO lastMessage) 
        : base(id, ConversationType.Private, lastMessage)
    {
        Recipient = recipient;
    }
}

public class GroupConversationBaseDTO : ConversationBaseDTO
{
    public string Name { get; }

    public GroupConversationBaseDTO(
        long id, 
        string name,
        MessageBaseDTO lastMessage) 
        : base(id, ConversationType.Group, lastMessage)
    {
        Name = name;
    }
}