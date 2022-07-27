using System;

namespace Rossgram.Domain.Errors.Access;

public class ForbiddenAccessError : Error
{
    public ForbiddenAccessError() 
        : base("Access denied")
    {
            
    }
}