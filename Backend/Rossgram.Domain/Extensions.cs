using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Rossgram.Domain;

public static class Extensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> item,
        out TKey outKey, out TValue outValue)
    {
        outKey = item.Key;
        outValue = item.Value;
    }

    public static void Deconstruct<TKey, TValue>(this IGrouping<TKey, TValue> item,
        out TKey outKey, out List<TValue> outValue)
    {
        outKey = item.Key;
        outValue = item.ToList();
    }
    
    public static Assembly GetAssembly(this AppDomain domain, string name)
    {
        return domain.GetAssemblies().Single(x => x.ManifestModule.Name == $"{name}.dll");
    }

    public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
    {
        return !source.Any();
    }
    
    public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
    {
        return !source.Contains(value);
    }
        
    public static bool IsSubclassOfRawGeneric(this Type? type, Type rawGeneric) {
        while (type != null && type != typeof(object)) {
            Type current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            if (rawGeneric == current) return true;
            type = type.BaseType;
        }
        return false;
    }

    public static string GetMemberName<TClass, TProp>(this Expression<Func<TClass, TProp>> lambda)
    {
        MemberExpression expression = (MemberExpression) lambda.Body;
        return expression.Member.Name;
    }
}