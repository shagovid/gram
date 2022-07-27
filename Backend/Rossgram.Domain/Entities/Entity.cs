using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rossgram.Domain.Entities;

public abstract class Entity : IEquatable<Entity>, IComparable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long  Id { get; private set; }

    protected Entity(long  id)
    {
        Id = id;
    }
        
    public override bool Equals(object? obj)
    {
        return obj is Entity otherValue && Equals(otherValue);
    }

    public bool Equals(Entity? other)
    {
        return other != null && GetType() == other.GetType() && Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        // Id is possible without a setter, but EF Core requests it
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Id.GetHashCode();
    }

    public int CompareTo(object? other)
    {
        return Id.CompareTo((other as Entity)?.Id);
    }
}