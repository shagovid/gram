using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(x => x.Owner)
            .WithMany(x => x.SentMessages)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Attachments)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.AfterEditMessage)
            .WithOne(x => x.BeforeEditMessage)
            .HasForeignKey<Message>(x => x.AfterEditMessageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Likes)
            .WithOne(x => x.Message)
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}