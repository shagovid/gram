using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class GroupConversationConfiguration : IEntityTypeConfiguration<GroupConversation>
{
    public void Configure(EntityTypeBuilder<GroupConversation> builder)
    {
        builder.HasMany(x => x.Members)
            .WithOne(x => x.GroupConversation)
            .HasForeignKey(x => x.GroupConversationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}