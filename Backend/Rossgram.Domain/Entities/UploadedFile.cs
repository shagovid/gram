using Rossgram.Domain.Enumerations;

namespace Rossgram.Domain.Entities;

public class UploadedFile : Entity
{
    public long OwnerId { get; set; }
    public Account Owner { get; set; }
    public string FullName { get; set; }
    public FileType Type { get; set; }
    public string ObjectsStorageKey { get; set; }
    
    #region EF Core constructor
#pragma warning disable 8618
    protected UploadedFile(long id) : base(id) { }
#pragma warning restore 8618
    #endregion
    
    public UploadedFile(
        long id, 
        long ownerId, 
        Account owner, 
        string fullName,  
        FileType type, 
        string objectsStorageKey) 
        : base(id)
    {
        OwnerId = ownerId;
        Owner = owner;
        FullName = fullName;
        Type = type;
        ObjectsStorageKey = objectsStorageKey;
    }
}