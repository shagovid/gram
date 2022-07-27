namespace Rossgram.Domain.Enumerations;

public class ConversationType : Enumeration
{
    public static ConversationType Private = new(1, "private");
    public static ConversationType Group = new(2, "group");
    
    public ConversationType(int id, string code) : base(id, code) { }
}