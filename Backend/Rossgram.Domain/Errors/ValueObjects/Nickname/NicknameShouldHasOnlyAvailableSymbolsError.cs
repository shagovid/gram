using System;
using System.Linq;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class NicknameShouldHasOnlyAvailableSymbolsError : Error
{
    public char[] AvailableSymbols { get; set; }
        
    public NicknameShouldHasOnlyAvailableSymbolsError(char[] availableSymbols) 
        : base($"Nickname can contains only " +
               $"english letters and {string.Join(", ", availableSymbols.Select(x => $"'{x}'"))}")
    {
        AvailableSymbols = availableSymbols;
    }
}