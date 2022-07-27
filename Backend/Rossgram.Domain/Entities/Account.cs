using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Rossgram.Domain.Entities.Messages;
using Rossgram.Domain.Entities.Posts;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Entities;

public class Account : Entity
{
    public Role Role { get; set; }
    public string Nickname { get; set; }
    public Password Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public long? AvatarId { get; set; }
    public UploadedFile? Avatar { get; set; }
    public bool IsVerified { get; set; }
    public ICollection<UploadedFile> UploadedFiles { get; set; }
    public ICollection<History> Histories { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<PostLike> PostsLikes { get; set; }
    public ICollection<PostComment> PostsComments { get; set; }
    public ICollection<PostCommentLike> PostCommentsLikes { get; set; }
    public ICollection<Following> Followings { get; set; }
    public ICollection<Following> Followers { get; set; }
    public ICollection<PrivateConversation> PrivateConversationsAsNewer { get; set; }
    public ICollection<PrivateConversation> PrivateConversationsAsOlder { get; set; }
    public ICollection<GroupConversationMember> GroupsConversationsMember { get; set; }
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<MessageLike> MessagesLikes { get; set; }

    #region EF Core constructor
#pragma warning disable 8618
    protected Account(long id) : base(id)
    {
    }
#pragma warning restore 8618
    #endregion

    public Account(
        long id, 
        Role role, 
        string nickname, 
        Password password, 
        string email, 
        string name, 
        string bio,
        long? avatarId,
        UploadedFile? avatar,
        bool isVerified,
        ICollection<UploadedFile> uploadedFiles,
        ICollection<History> histories,
        ICollection<Post> posts, 
        ICollection<PostLike> postsLikes, 
        ICollection<PostComment> postsComments, 
        ICollection<PostCommentLike> postCommentsLikes, 
        ICollection<Following> followings, 
        ICollection<Following> followers, 
        ICollection<PrivateConversation> privateConversationsAsNewer, 
        ICollection<PrivateConversation> privateConversationsAsOlder, 
        ICollection<GroupConversationMember> groupsConversationsMember,
        ICollection<Message> sentMessages, 
        ICollection<MessageLike> messagesLikes) 
        : base(id)
    {
        Role = role;
        Nickname = nickname;
        Password = password;
        Email = email;
        Name = name;
        Bio = bio;
        AvatarId = avatarId;
        Avatar = avatar;
        IsVerified = isVerified;
        UploadedFiles = uploadedFiles;
        Histories = histories;
        Posts = posts;
        PostsLikes = postsLikes;
        PostsComments = postsComments;
        PostCommentsLikes = postCommentsLikes;
        Followings = followings;
        Followers = followers;
        PrivateConversationsAsNewer = privateConversationsAsNewer;
        PrivateConversationsAsOlder = privateConversationsAsOlder;
        GroupsConversationsMember = groupsConversationsMember;
        SentMessages = sentMessages;
        MessagesLikes = messagesLikes;
    }
}