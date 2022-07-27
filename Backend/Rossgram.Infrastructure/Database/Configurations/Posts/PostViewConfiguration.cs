using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Views;

namespace Rossgram.Infrastructure.Database.Configurations.Posts;

public class PostViewConfiguration : IEntityTypeConfiguration<PostView>
{
    public void Configure(EntityTypeBuilder<PostView> builder)
    {
        builder.ToView(DatabaseView.PostViewConfiguration.Name);
        
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Attachments)
            .WithOne()
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasMany(x => x.Likes)
            .WithOne()
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasMany(x => x.Comments)
            .WithOne()
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}