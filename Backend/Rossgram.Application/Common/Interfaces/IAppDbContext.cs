using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Views;

namespace Rossgram.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Account> Accounts { get; }
    DbSet<AccountView> AccountsView { get; }
    DbSet<ReservedNickname> ReservedNicknames { get; }
    DbSet<Following> Followings { get; }
    DbSet<UploadedFile> UploadedFiles { get; }
    DbSet<History> Histories { get; }
    DbSet<Post> Posts { get; }
    DbSet<PostView> PostsView { get; }
    DbSet<PostAttachment> PostsAttachments { get; }
    DbSet<PostLike> PostsLikes { get; }
    DbSet<PostComment> PostsComments { get; }
    DbSet<PostCommentView> PostsCommentsView { get; }
    DbSet<PostCommentLike> PostsCommentsLikes { get; }
    DbSet<Conversation> Conversations { get; }
    DbSet<GroupConversationMember> GroupsConversationsMembers { get; }
    DbSet<Message> Messages { get; }
    DbSet<MessageView> MessagesView { get; }
    DbSet<MessageLike> MessagesLikes { get; }
    DbSet<LogRecord> Logs { get; }

    Task AutoMigrationAsync();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}