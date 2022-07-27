namespace Rossgram.Domain.Enumerations;

public class Permission : Enumeration
{
    public static Permission CanCreateAdmin = new Permission(1, "can_create_admin");
    public static Permission CanCreateModer = new Permission(2, "can_create_moder");
    public static Permission CanChangeIsVerified = new Permission(3, "can_change_is_verified");
    public static Permission CanRemovePosts = new Permission(4, "can_remove_posts");
    public static Permission CanRemovePostComments = new Permission(5, "can_remove_post_comments");

    public Permission(int id, string code) : base(id, code) { }
}