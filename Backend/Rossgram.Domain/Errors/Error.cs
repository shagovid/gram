using System;

namespace Rossgram.Domain.Errors;

public abstract class Error : Exception
{
    protected Error(string message, Exception? innerException = null) 
        : base(message, innerException)
    {

    }
}