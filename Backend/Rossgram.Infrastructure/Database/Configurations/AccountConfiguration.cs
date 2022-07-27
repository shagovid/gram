using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Infrastructure.Database.Conversions;

namespace Rossgram.Infrastructure.Database.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(x => x.Nickname).IsUnique();
        builder.OwnsOne(x => x.Password);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasOne(x => x.Avatar)
            .WithOne()
            .HasForeignKey<Account>(x => x.AvatarId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.UploadedFiles)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostsLikes)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostsComments)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostCommentsLikes)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Followings)
            .WithOne(x => x.Follower)
            .HasForeignKey(x => x.FollowerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Followers)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PrivateConversationsAsOlder)
            .WithOne(x => x.OlderAccount)
            .HasForeignKey(x => x.OlderAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PrivateConversationsAsNewer)
            .WithOne(x => x.NewerAccount)
            .HasForeignKey(x => x.NewerAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.GroupsConversationsMember)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.SentMessages)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.MessagesLikes)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}