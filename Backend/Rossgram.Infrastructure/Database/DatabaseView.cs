using System.Collections.Generic;
using System.Linq;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.ValueObjects;
using Rossgram.Domain.Views;

namespace Rossgram.Infrastructure.Database;

public class DatabaseView
{
    public static readonly DatabaseView AccountViewConfiguration = new DatabaseView(
        name: nameof(AccountView),
        selection: new Dictionary<string, string>
        {
            [nameof(AccountView.Id)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Id)}\"",
            [nameof(AccountView.Role)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Role)}\"",
            [nameof(AccountView.Nickname)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Nickname)}\"",
            [$"{nameof(AccountView.Password)}_{nameof(Password.Hash)}"] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Password)}_{nameof(Password.Hash)}\"",
            [$"{nameof(AccountView.Password)}_{nameof(Password.Salt)}"] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Password)}_{nameof(Password.Salt)}\"",
            [nameof(AccountView.Email)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Email)}\"",
            [nameof(AccountView.Name)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Name)}\"",
            [nameof(AccountView.Bio)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Bio)}\"",
            [nameof(AccountView.AvatarId)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.AvatarId)}\"",
            [nameof(AccountView.IsVerified)] = 
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.IsVerified)}\"",
            [nameof(AccountView.FollowerCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.Followings)}\" fer " +
                $"WHERE fer.\"{nameof(Following.AccountId)}\" = " +
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Id)}\")",
            [nameof(AccountView.FollowingCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.Followings)}\" fing " +
                $"WHERE fing.\"{nameof(Following.FollowerId)}\" = " +
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Id)}\")",
            [nameof(AccountView.PostsCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.Posts)}\" pst " +
                $"WHERE pst.\"{nameof(Post.OwnerId)}\" = " +
                $"\"{nameof(AppDbContext.Accounts)}\".\"{nameof(Account.Id)}\")",
        },
        fromTable: nameof(AppDbContext.Accounts)
    );
    
    public static readonly DatabaseView PostViewConfiguration = new DatabaseView(
        name: nameof(PostView),
        selection: new Dictionary<string, string>
        {
            [nameof(PostView.Id)] = 
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.Id)}\"",
            [nameof(PostView.OwnerId)] = 
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.OwnerId)}\"",
            [nameof(PostView.CreatedAt)] = 
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.TimeStamp)}\"",
            [nameof(PostView.Comment)] = 
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.Comment)}\"",
            [nameof(PostView.LikesCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.PostsLikes)}\" plike " +
                $"WHERE plike.\"{nameof(PostLike.PostId)}\" = " +
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.Id)}\")",
            [nameof(PostView.CommentsCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.PostsComments)}\" pcomm " +
                $"WHERE pcomm.\"{nameof(PostComment.PostId)}\" = " +
                $"\"{nameof(AppDbContext.Posts)}\".\"{nameof(Post.Id)}\")",
        },
        fromTable: nameof(AppDbContext.Posts)
    );
        
    public static readonly DatabaseView PostCommentViewConfiguration = new DatabaseView(
        name: nameof(PostCommentView),
        selection: new Dictionary<string, string>
        {
            [nameof(PostCommentView.Id)] = 
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.Id)}\"",
            [nameof(PostCommentView.OwnerId)] = 
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.OwnerId)}\"",
            [nameof(PostCommentView.PostId)] = 
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.PostId)}\"",
            [nameof(PostCommentView.CreatedAt)] = 
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.TimeStamp)}\"",
            [nameof(PostCommentView.Text)] = 
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.Text)}\"",
            [nameof(PostCommentView.LikesCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.PostsCommentsLikes)}\" pclike " +
                $"WHERE pclike.\"{nameof(PostCommentLike.CommentId)}\" = " +
                $"\"{nameof(AppDbContext.PostsComments)}\".\"{nameof(PostComment.Id)}\")",
        },
        fromTable: nameof(AppDbContext.PostsComments)
    );
        
    public static readonly DatabaseView MessageViewConfiguration = new DatabaseView(
        name: nameof(MessageView),
        selection: new Dictionary<string, string>
        {
            [nameof(MessageView.Id)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.Id)}\"",
            [nameof(MessageView.OwnerId)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.OwnerId)}\"",
            [nameof(MessageView.ConversationId)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.ConversationId)}\"",
            [nameof(MessageView.TimeStamp)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.CreatedAt)}\"",
            [nameof(MessageView.Text)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.Text)}\"",
            [nameof(MessageView.AfterEditMessageId)] = 
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.AfterEditMessageId)}\"",
            [nameof(MessageView.LikesCount)] = 
                $"(SELECT Count(*) " +
                $"FROM \"{nameof(AppDbContext.MessagesLikes)}\" mlike " +
                $"WHERE mlike.\"{nameof(MessageLike.MessageId)}\" = " +
                $"\"{nameof(AppDbContext.Messages)}\".\"{nameof(Message.Id)}\")",
        },
        fromTable: nameof(AppDbContext.Messages)
    );
    
    public string Name { get; }
    public Dictionary<string, string> Selection { get; }
    public string FromTable { get; }

    public DatabaseView(
        string name, 
        Dictionary<string, string> selection, 
        string fromTable)
    {
        Name = name;
        Selection = selection;
        FromTable = fromTable;
    }

    private string GetColumnsNamesSql()
    {
        return string.Join(", ", Selection.Select(x => $"{x.Value} AS \"{x.Key}\""));
    }
        
    public string GetCreateSql()
    {
        return $"CREATE VIEW \"{Name}\" AS " +
               $"SELECT {GetColumnsNamesSql()} FROM \"{FromTable}\"";
    }

    public string GetDropSql()
    {
        return $"DROP VIEW IF EXISTS \"{Name}\"";
    }
}