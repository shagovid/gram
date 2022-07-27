namespace Rossgram.Domain.Errors.ValueObjects.Email;

public class EmailShouldBeUniqueError: Error
{
    public EmailShouldBeUniqueError() 
        : base("Email should be unique")
    {
            
    }
}