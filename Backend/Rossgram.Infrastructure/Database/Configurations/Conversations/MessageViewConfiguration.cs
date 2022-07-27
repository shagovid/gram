using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Views;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageViewConfiguration : IEntityTypeConfiguration<MessageView>
{
    public void Configure(EntityTypeBuilder<MessageView> builder)
    {
        builder.ToView(DatabaseView.MessageViewConfiguration.Name);
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Conversation)
            .WithMany()
            .HasForeignKey(x => x.ConversationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Attachments)
            .WithOne()
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.AfterEditMessage)
            .WithOne()
            .HasForeignKey<Message>(x => x.AfterEditMessageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.BeforeEditMessage)
            .WithOne()
            .HasForeignKey<Message>(x => x.AfterEditMessageId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Likes)
            .WithOne()
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}