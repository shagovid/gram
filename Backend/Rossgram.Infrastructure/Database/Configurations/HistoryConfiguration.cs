using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;

namespace Rossgram.Infrastructure.Database.Configurations;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Histories)
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x => x.UploadedFile)
            .WithOne()
            .HasForeignKey<History>(x => x.UploadedFileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}