using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Domain.Enumerations;

public abstract class Enumeration : ValueObject, IEquatable<Enumeration>, IComparable
{
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        FieldInfo[] fields = typeof(T).GetFields(
            bindingAttr: BindingFlags.Public | 
                         BindingFlags.Static | 
                         BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static bool TryGetById<T>(int id, out T? role) where T : Enumeration
    {
        role = GetAll<T>().FirstOrDefault(x => x.Id == id);
        return role is not null;
    }

    public static T GetById<T>(int id) where T : Enumeration
    {
        if (TryGetById(id, out T? value)) return value!;
        throw new ArgumentException($"Invalid id '{id}' for {typeof(T).Name}");
    }

    public static bool TryGetByCode<T>(string code, out T? role) where T : Enumeration
    {
        role = GetAll<T>().FirstOrDefault(x => x.Code == code);
        return role is not null;
    }
        
    public static T GetByCode<T>(string code) where T : Enumeration
    {
        if (TryGetByCode(code, out T? value)) return value!;
        throw new ArgumentException($"Invalid code '{code}' for {typeof(T).Name}");
    }
        
    public int Id { get; }
    public string Code { get; }

    protected Enumeration(int id, string code)
    {
        Id = id;
        Code = code;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Id;
        yield return Code;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Code);
    }
        
    public virtual bool Equals(Enumeration? other)
    {
        return other != null && GetType() == other.GetType() && Id == other.Id;
    }

    public int CompareTo(object? other)
    {
        return Id.CompareTo((other as Enumeration)?.Id);
    }

    public override string ToString() => Code;
}