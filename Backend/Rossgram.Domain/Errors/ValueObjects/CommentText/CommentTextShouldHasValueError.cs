namespace Rossgram.Domain.Errors.ValueObjects.CommentText;

public class CommentTextShouldHasValueError : Error
{
    public CommentTextShouldHasValueError() 
        : base("Comment text cannot be empty")
    {
            
    }
}