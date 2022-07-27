using System;

namespace Rossgram.Domain.Errors.ValueObjects.Nickname;

public class NicknameShouldBeUniqueError : Error
{
    public NicknameShouldBeUniqueError() 
        : base("Nickname should be unique")
    {
            
    }
}