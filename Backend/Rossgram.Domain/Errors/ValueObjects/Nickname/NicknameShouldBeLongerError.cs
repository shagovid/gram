using System;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class NicknameShouldBeLongerError : Error
{
    public int MinLength { get; set; }
        
    public NicknameShouldBeLongerError(int minLength) 
        : base($"Nickname must be at least {minLength} characters")
    {
            
    }
}