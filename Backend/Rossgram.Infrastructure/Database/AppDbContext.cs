using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Views;
using Rossgram.Infrastructure.Database.Conversions;
using Serilog;

namespace Rossgram.Infrastructure.Database;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<AccountView> AccountsView { get; set; } = null!;
    public DbSet<ReservedNickname> ReservedNicknames { get; set; } = null!;
    public DbSet<Following> Followings { get; set; } = null!;
    public DbSet<UploadedFile> UploadedFiles { get; set; } = null!;
    public DbSet<History> Histories { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostView> PostsView { get; set; } = null!;
    public DbSet<PostAttachment> PostsAttachments { get; set; } = null!;
    public DbSet<PostLike> PostsLikes { get; set; } = null!;
    public DbSet<PostComment> PostsComments { get; set; } = null!;
    public DbSet<PostCommentView> PostsCommentsView { get; set; } = null!;
    public DbSet<PostCommentLike> PostsCommentsLikes { get; set; } = null!;
    public DbSet<Conversation> Conversations { get; set; } = null!;
    public DbSet<GroupConversationMember> GroupsConversationsMembers { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<MessageView> MessagesView { get; set; } = null!;
    public DbSet<MessageLike> MessagesLikes { get; set; } = null!;
    public DbSet<LogRecord> Logs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
            
        modelBuilder.ApplyConfigurationsFromAssembly(
            assembly: Assembly.GetAssembly(typeof(AppDbContext))!);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<AttachmentType>().HaveConversion<EnumerationConverter<AttachmentType>>();
        configurationBuilder.Properties<ConversationType>().HaveConversion<EnumerationConverter<ConversationType>>();
        configurationBuilder.Properties<FileType>().HaveConversion<EnumerationConverter<FileType>>();
        configurationBuilder.Properties<Permission>().HaveConversion<EnumerationConverter<Permission>>();
        configurationBuilder.Properties<Role>().HaveConversion<EnumerationConverter<Role>>();
    }

    public async Task AutoMigrationAsync()
    {
        Log.Information("Applying migrations");
        
#if DEBUG
        // Drop database at start
        await this.Database.EnsureDeletedAsync();
#endif
        await this.Database.MigrateAsync();

        await this.Database.EnsureCreateViewAsync(DatabaseView.AccountViewConfiguration);
        await this.Database.EnsureCreateViewAsync(DatabaseView.PostViewConfiguration);
        await this.Database.EnsureCreateViewAsync(DatabaseView.PostCommentViewConfiguration);
        await this.Database.EnsureCreateViewAsync(DatabaseView.MessageViewConfiguration);
    }
}