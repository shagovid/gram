using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Views;
using Rossgram.Infrastructure.Database.Conversions;

namespace Rossgram.Infrastructure.Database.Configurations;

public class AccountViewConfiguration : IEntityTypeConfiguration<AccountView>
{
    public void Configure(EntityTypeBuilder<AccountView> builder)
    {
        builder.ToView(DatabaseView.AccountViewConfiguration.Name);
        builder.HasIndex(x => x.Nickname).IsUnique();
        builder.OwnsOne(x => x.Password);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasOne(x => x.Avatar)
            .WithOne()
            .HasForeignKey<AccountView>(x => x.AvatarId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.UploadedFiles)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Posts)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostsLikes)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostsComments)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PostCommentsLikes)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Followings)
            .WithOne()
            .HasForeignKey(x => x.FollowerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Followers)
            .WithOne()
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PrivateConversationsAsOlder)
            .WithOne()
            .HasForeignKey(x => x.OlderAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.PrivateConversationsAsNewer)
            .WithOne()
            .HasForeignKey(x => x.NewerAccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.GroupsConversationsMember)
            .WithOne()
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.SentMessages)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.MessagesLikes)
            .WithOne()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}