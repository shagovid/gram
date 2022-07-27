namespace Rossgram.Domain.Entities.Messages;

public class GroupConversationMember : Entity
{
    public long GroupConversationId { get; set; }
    public GroupConversation GroupConversation { get; set; }
    public long AccountId { get; set; }
    public Account Account { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected GroupConversationMember(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public GroupConversationMember(
        long id, 
        long groupConversationId, 
        GroupConversation groupConversation, 
        long accountId, 
        Account account) 
        : base(id)
    {
        GroupConversationId = groupConversationId;
        GroupConversation = groupConversation;
        AccountId = accountId;
        Account = account;
    }
}