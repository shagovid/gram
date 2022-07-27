using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageAttachmentConfiguration : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasOne(x => x.Message)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasDiscriminator(x => x.Type)
            .HasValue<MessageFileAttachment>(AttachmentType.File)
            .HasValue<MessageLinkAttachment>(AttachmentType.Link)
            .HasValue<MessagePostAttachment>(AttachmentType.Post)
            .HasValue<MessageHistoryAttachment>(AttachmentType.History);
    }
}