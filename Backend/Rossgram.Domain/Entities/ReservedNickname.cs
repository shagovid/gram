namespace Rossgram.Domain.Entities;

public class ReservedNickname : Entity
{
    public string Nickname { get; set; }
    public string Key { get; set; }

    #region EF Core constructor
#pragma warning disable 8618
    protected ReservedNickname(long id) : base(id) { }
#pragma warning restore 8618
    #endregion

    public ReservedNickname(
        long id,
        string nickname,
        string key) 
        : base(id)
    {
        Nickname = nickname;
        Key = key;
    }
}