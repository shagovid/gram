using System;

namespace Rossgram.Domain.Enumerations;

public class Role : Enumeration
{
    public static Role Admin = new(id: 1, code: "admin", new []
    {
        Permission.CanCreateAdmin, 
        Permission.CanCreateModer,
        Permission.CanChangeIsVerified, 
        Permission.CanRemovePosts,
        Permission.CanRemovePostComments,
    });
    public static Role Moderator = new(id: 2, code: "moderator", new []
    {
        Permission.CanRemovePosts,
        Permission.CanRemovePostComments,
        Permission.CanChangeIsVerified, 
    });
    public static Role User = new(id: 3, code: "user", Array.Empty<Permission>());

    public Permission[] Permissions { get; }
        
    protected Role(int id, string code, Permission[] permissions) 
        : base(id, code)
    {
        Permissions = permissions;
    }
}