using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities.Messages;

public class MessageHistoryAttachment : MessageAttachment
{
    public long HistoryId { get; set; }
    public History History { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected MessageHistoryAttachment(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public MessageHistoryAttachment(
        long id, 
        long messageId,
        Message message, 
        int order,
        long historyId,
        History history) 
        : base(id, messageId, message, order, AttachmentType.History)
    {
        HistoryId = historyId;
        History = history;
    }
}