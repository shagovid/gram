using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Infrastructure.Database.Conversions;

public class EnumerationConverter<T> : ValueConverter<T, int>
    where T: Enumeration
{
    public EnumerationConverter()
        : base(
            v => v.Id,
            v => Enumeration.GetById<T>(v))
    {
    }
}