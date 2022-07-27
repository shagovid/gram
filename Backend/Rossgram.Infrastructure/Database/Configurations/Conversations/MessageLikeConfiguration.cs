using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities.Messages;

namespace Rossgram.Infrastructure.Database.Configurations.Conversations;

public class MessageLikeConfiguration : IEntityTypeConfiguration<MessageLike>
{
    public void Configure(EntityTypeBuilder<MessageLike> builder)
    {
        builder.HasOne(x => x.Owner)
            .WithMany(x => x.MessagesLikes)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Message)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.MessageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}