using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class PrivateConversationConfiguration : IEntityTypeConfiguration<PrivateConversation>
{
    public void Configure(EntityTypeBuilder<PrivateConversation> builder)
    {
        builder.HasOne(x => x.OlderAccount)
            .WithMany(x => x.PrivateConversationsAsOlder)
            .HasForeignKey(x => x.OlderAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.NewerAccount)
            .WithMany(x => x.PrivateConversationsAsNewer)
            .HasForeignKey(x => x.NewerAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}