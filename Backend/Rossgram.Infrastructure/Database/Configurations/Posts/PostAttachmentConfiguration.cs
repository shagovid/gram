using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Infrastructure.Database.Configurations.Posts;

public class PostAttachmentConfiguration : IEntityTypeConfiguration<PostAttachment>
{
    public void Configure(EntityTypeBuilder<PostAttachment> builder)
    {
        builder.HasOne(x => x.Post)
            .WithMany(x => x.Attachments)
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasDiscriminator(x => x.AttachmentType)
            .HasValue<PostAttachmentFile>(AttachmentType.File)
            .HasValue<PostAttachmentLink>(AttachmentType.Link);
    }
}