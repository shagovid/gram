using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class GroupConversationMemberConfiguration : IEntityTypeConfiguration<GroupConversationMember>
{
    public void Configure(EntityTypeBuilder<GroupConversationMember> builder)
    {
        builder.HasOne(x => x.GroupConversation)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.GroupConversationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Account)
            .WithMany(x => x.GroupsConversationsMember)
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}