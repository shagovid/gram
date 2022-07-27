using System;
using System.Collections.Generic;
using System.Linq;

namespace Rossgram.Domain.ValueObjects;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object?> GetAtomicValues();
        
    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
        
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        ValueObject other = (ValueObject)obj;
        using IEnumerator<object?> thisValues = GetAtomicValues().GetEnumerator();
        using IEnumerator<object?> otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^
                ReferenceEquals(otherValues.Current, null))
            {
                return false;
            }

            if (thisValues.Current != null &&
                !thisValues.Current.Equals(otherValues.Current))
            {
                return false;
            }
        }
        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public virtual bool Equals(ValueObject? other)
    {
        return Equals((object?) other);
    }
        
    public static bool operator ==(ValueObject? x, ValueObject? y)
    {
        return x?.Equals(y) ?? y is null;
    }

    public static bool operator !=(ValueObject? x, ValueObject? y)
    {
        return !(x == y);
    }
}