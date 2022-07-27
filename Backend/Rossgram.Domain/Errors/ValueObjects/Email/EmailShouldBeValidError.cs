namespace Rossgram.Domain.Errors.ValueObjects.Email;

public class EmailShouldBeValidError: Error
{
    public EmailShouldBeValidError() 
        : base("Email should be valid")
    {
            
    }
}