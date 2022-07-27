namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IUsernameConfig
{
    public int MinLength { get; }
    public char[] AvailableSymbols { get; }
}