using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Views;

namespace Rossgram.Infrastructure.Database.Configurations.Posts;

public class PostCommentViewConfiguration : IEntityTypeConfiguration<PostCommentView>
{
    public void Configure(EntityTypeBuilder<PostCommentView> builder)
    {
        builder.ToView(DatabaseView.PostCommentViewConfiguration.Name);
        
        
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x => x.Post)
            .WithMany()
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x => x.Likes)
            .WithOne()
            .HasForeignKey(x => x.CommentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}