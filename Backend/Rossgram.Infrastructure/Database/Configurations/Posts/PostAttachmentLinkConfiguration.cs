using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Posts;

namespace Rossgram.Infrastructure.Database.Configurations.Posts;

public class PostAttachmentLinkConfiguration : IEntityTypeConfiguration<PostAttachmentLink>
{
    public void Configure(EntityTypeBuilder<PostAttachmentLink> builder)
    {
        
    }
}