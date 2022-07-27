using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Posts;

namespace Rossgram.Infrastructure.Database.Configurations.Posts;

public class PostAttachmentFileConfiguration : IEntityTypeConfiguration<PostAttachmentFile>
{
    public void Configure(EntityTypeBuilder<PostAttachmentFile> builder)
    {
        builder.HasOne(x => x.UploadedFile)
            .WithOne()
            .HasForeignKey<PostAttachmentFile>(x => x.UploadedFileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}