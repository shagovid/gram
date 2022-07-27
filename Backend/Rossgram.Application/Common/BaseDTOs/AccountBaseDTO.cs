using System.Collections.Generic;
using Rossgram.Domain.Entities;

namespace Rossgram.Application.Common.BaseDTOs;

public class AccountShortBaseDTO : BaseDTO
{
    public string Nickname { get; }
    public bool IsVerified { get; }
    public string? AvatarLink { get; }

    public AccountShortBaseDTO(
        long id, 
        string nickname, 
        bool isVerified, 
        string? avatarLink) 
        : base(id)
    {
        Nickname = nickname;
        IsVerified = isVerified;
        AvatarLink = avatarLink;
    }
}

public class AccountBaseDTO : AccountShortBaseDTO
{
    public string Name { get; }
    public string Bio { get; }
    public int FollowerCount { get; }
    public int FollowingCount { get; }
    public int PostsCount { get; }

    public AccountBaseDTO(
        long id, 
        string nickname, 
        bool isVerified, 
        string? avatarLink, 
        string name, 
        string bio, 
        int followerCount, 
        int followingCount, 
        int postsCount) 
        : base(id, nickname, isVerified, avatarLink)
    {
        Name = name;
        Bio = bio;
        FollowerCount = followerCount;
        FollowingCount = followingCount;
        PostsCount = postsCount;
    }
}