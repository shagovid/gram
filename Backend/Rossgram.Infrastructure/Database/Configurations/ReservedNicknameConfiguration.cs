using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;

namespace Rossgram.Infrastructure.Database.Configurations;

public class ReservedNicknameConfiguration : IEntityTypeConfiguration<ReservedNickname>
{
    public void Configure(EntityTypeBuilder<ReservedNickname> builder)
    {
        builder.HasIndex(x => x.Nickname).IsUnique();
    }
}