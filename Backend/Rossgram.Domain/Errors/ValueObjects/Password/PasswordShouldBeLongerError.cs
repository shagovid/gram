using System;

namespace Rossgram.Domain.Errors.ValueObjects.Password;

public class PasswordShouldBeLongerError : Error
{
    public int MinLength { get; set; }
        
    public PasswordShouldBeLongerError(int minLength) 
        : base($"Password must be at least {minLength} characters")
    {
            
    }
}