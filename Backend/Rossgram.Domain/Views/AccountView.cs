using System.Collections.Generic;
using Rossgram.Domain.Entities;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Views;

public class AccountView
{
    public long Id { get; set; }
    public Role Role { get; set; } = null!;
    public string Nickname { get; set; } = null!;
    public Password Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public long? AvatarId { get; set; }
    public UploadedFile? Avatar { get; set; }
    public bool IsVerified { get; set; }
    public ICollection<UploadedFile> UploadedFiles { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = null!;
    public int PostsCount { get; set; }
    public ICollection<PostLike> PostsLikes { get; set; } = null!;
    public ICollection<PostComment> PostsComments { get; set; } = null!;
    public ICollection<PostCommentLike> PostCommentsLikes { get; set; } = null!;
    public ICollection<Following> Followings { get; set; } = null!;
    public int FollowingCount { get; set; }
    public ICollection<Following> Followers { get; set; } = null!;
    public int FollowerCount { get; set; }
    public ICollection<PrivateConversation> PrivateConversationsAsNewer { get; set; } = null!;
    public ICollection<PrivateConversation> PrivateConversationsAsOlder { get; set; } = null!;
    public ICollection<GroupConversationMember> GroupsConversationsMember { get; set; } = null!;
    public ICollection<Message> SentMessages { get; set; } = null!;
    public ICollection<MessageLike> MessagesLikes { get; set; } = null!;
}