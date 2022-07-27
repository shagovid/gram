namespace Rossgram.Domain.Errors.ValueObjects.CommentText;

public class CommentTextShouldBeShorterError : Error
{
    public int MaxLength { get; }

    public CommentTextShouldBeShorterError(int maxLength) 
        : base($"Comment text must be shorter than {maxLength} symbols")
    {
        MaxLength = maxLength;
    }
}