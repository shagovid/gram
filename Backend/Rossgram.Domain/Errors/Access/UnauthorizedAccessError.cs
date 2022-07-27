using System;

namespace Rossgram.Domain.Errors.Access;

public class UnauthorizedAccessError : Error
{
    public UnauthorizedAccessError() 
        : base("Authorization required")
    {
            
    }
}