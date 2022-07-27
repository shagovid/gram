namespace Rossgram.Domain.Enumerations;

public class AttachmentType : Enumeration
{
    public static AttachmentType File = new(1, "file");
    public static AttachmentType Link = new(2, "link");
    public static AttachmentType Post = new(3, "post");
    public static AttachmentType History = new(4, "history");
    
    public AttachmentType(int id, string code) : base(id, code) { }
}