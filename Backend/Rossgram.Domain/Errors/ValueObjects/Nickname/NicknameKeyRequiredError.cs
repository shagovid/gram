using System;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class NicknameKeyRequiredError : Error
{
    public string Nickname { get; set; }
    
    public NicknameKeyRequiredError(string nickname) 
        : base($"Nickname '{nickname}' required key")
    {
        Nickname = nickname;
    }
}