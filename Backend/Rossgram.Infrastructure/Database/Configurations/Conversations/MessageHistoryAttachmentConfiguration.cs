using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageHistoryAttachmentConfiguration : IEntityTypeConfiguration<MessageHistoryAttachment>
{
    public void Configure(EntityTypeBuilder<MessageHistoryAttachment> builder)
    {
        builder.HasOne(x => x.History)
            .WithMany(x => x.MessagesAttachments)
            .HasForeignKey(x => x.HistoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}