using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessagePostAttachmentConfiguration : IEntityTypeConfiguration<MessagePostAttachment>
{
    public void Configure(EntityTypeBuilder<MessagePostAttachment> builder)
    {
        builder.HasOne(x => x.Post)
            .WithMany(x => x.MessagesAttachments)
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}