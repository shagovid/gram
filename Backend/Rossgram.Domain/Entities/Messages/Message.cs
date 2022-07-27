using System;
using System.Collections.Generic;

namespace Rossgram.Domain.Entities.Messages;

public class Message : Entity
{
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
    public long ConversationId { get; set; }
    public Conversation Conversation { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Text { get; set; }
    public ICollection<MessageAttachment> Attachments { get; set; }
    public long? AfterEditMessageId { get; set; }
    public Message? AfterEditMessage { get; set; }
    public Message? BeforeEditMessage { get; set; }
    public ICollection<MessageLike> Likes { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected Message(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public Message(
        long id, 
        long ownerId, 
        Account owner, 
        long conversationId, 
        Conversation conversation, 
        DateTimeOffset createdAt, 
        string text, 
        ICollection<MessageAttachment> attachments,
        long? afterEditMessageId,
        Message? afterEditMessage,
        Message? beforeEditMessage,
        ICollection<MessageLike> likes) 
        : base(id)
    {
        OwnerId = ownerId;
        Owner = owner;
        ConversationId = conversationId;
        Conversation = conversation;
        CreatedAt = createdAt;
        Text = text;
        Attachments = attachments;
        AfterEditMessageId = afterEditMessageId;
        AfterEditMessage = afterEditMessage;
        BeforeEditMessage = beforeEditMessage;
        Likes = likes;
    }
}