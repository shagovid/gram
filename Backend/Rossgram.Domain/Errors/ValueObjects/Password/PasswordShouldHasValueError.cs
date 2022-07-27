using System;

namespace Rossgram.Domain.Errors.ValueObjects.Password;

public class PasswordShouldHasValueError : Error
{
    public PasswordShouldHasValueError() 
        : base("Password cannot be empty")
    {
            
    }
}