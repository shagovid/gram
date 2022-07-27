using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageFileAttachmentConfiguration : IEntityTypeConfiguration<MessageFileAttachment>
{
    public void Configure(EntityTypeBuilder<MessageFileAttachment> builder)
    {
        builder.HasOne(x => x.UploadedFile)
            .WithOne()
            .HasForeignKey<MessageFileAttachment>(x => x.UploadedFileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}