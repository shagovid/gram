using System;

namespace Rossgram.Domain.Errors.ValueObjects.Email;

public class EmailShouldHasValueError : Error
{
    public EmailShouldHasValueError() 
        : base("Email cannot be empty")
    {
            
    }
}