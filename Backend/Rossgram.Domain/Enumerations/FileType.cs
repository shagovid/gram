namespace Rossgram.Domain.Enumerations;

public class FileType : Enumeration
{
    public static FileType Photo = new(1, "photo");
    public static FileType Video = new(2, "video");
    public static FileType Audio = new(3, "audio");
    public static FileType Document = new(4, "doc");
    public static FileType Other = new(5, "file");
    
    public FileType(int id, string code) : base(id, code) { }
}