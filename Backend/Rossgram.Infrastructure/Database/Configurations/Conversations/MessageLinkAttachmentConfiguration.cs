using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageLinkAttachmentConfiguration : IEntityTypeConfiguration<MessageLinkAttachment>
{
    public void Configure(EntityTypeBuilder<MessageLinkAttachment> builder)
    {
        
    }
}