using System;
using System.Collections.Generic;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;

namespace Rossgram.Domain.Views;

public class MessageView
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public Account Owner { get; set; } = null!;
    public long ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;
    public DateTimeOffset TimeStamp { get; set; }
    public string Text { get; set; } = null!;
    public ICollection<MessageAttachment> Attachments { get; set; } = null!;
    public long? AfterEditMessageId { get; set; }
    public Message? AfterEditMessage { get; set; }
    public Message? BeforeEditMessage { get; set; }
    public ICollection<MessageLike> Likes { get; set; } = null!;
    public int LikesCount { get; set; }
}