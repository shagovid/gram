using System;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class NicknameShouldHasValueError : Error
{
    public NicknameShouldHasValueError() 
        : base("Nickname cannot be empty")
    {
            
    }
}