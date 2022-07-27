namespace Rossgram.Application.Common.BaseDTOs;

public class BaseDTO
{
    public long Id { get; }

    protected BaseDTO(long id)
    {
        Id = id;
    }
}