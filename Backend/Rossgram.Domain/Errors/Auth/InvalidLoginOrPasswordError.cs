using System;

namespace Rossgram.Domain.Errors.Auth;

public class InvalidLoginOrPasswordError : Error
{
    public InvalidLoginOrPasswordError() 
        : base("Invalid login or password")
    {
            
    }
}