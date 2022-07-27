using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Enumerations;
using Rossgram.Infrastructure.Database.Conversions;

namespace Rossgram.Infrastructure.Database.Configurations;

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder.HasOne(x => x.Account)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x => x.Follower)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}