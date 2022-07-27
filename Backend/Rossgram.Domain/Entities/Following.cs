namespace Rossgram.Domain.Entities;

public class Following : Entity
{
    public long AccountId { get; set; }
    public Account Account { get; set; }
    public long FollowerId { get; set; }
    public Account Follower { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected Following(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public Following(
        long id, 
        long accountId, 
        Account account, 
        long followerId, 
        Account follower) 
        : base(id)
    {
        AccountId = accountId;
        Account = account;
        FollowerId = followerId;
        Follower = follower;
    }
}