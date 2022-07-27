using System;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class InvalidNicknameKeyError : Error
{
    public string Nickname { get; set; }
    
    public InvalidNicknameKeyError(string nickname) 
        : base($"Invalid key for nickname '{nickname}'")
    {
        Nickname = nickname;
    }
}