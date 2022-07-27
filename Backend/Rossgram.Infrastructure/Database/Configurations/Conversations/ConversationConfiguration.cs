using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.HasDiscriminator(x => x.ConversationType)
            .HasValue<PrivateConversation>(ConversationType.Private)
            .HasValue<GroupConversation>(ConversationType.Group);
        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Conversation)
            .HasForeignKey(x => x.ConversationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}