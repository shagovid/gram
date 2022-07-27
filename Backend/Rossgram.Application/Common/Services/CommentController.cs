using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Errors.ValueObjects.CommentText;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Common.Services;

public class CommentController
{
    private readonly ICommentConfig _config;

    public CommentController(
        ICommentConfig config)
    {
        _config = config;
    }

    public string Validate(string? value)
    {
        value = value?.Trim();
        
        if (string.IsNullOrEmpty(value)) 
            throw new CommentTextShouldHasValueError();
        if (value.Length > _config.TextMaxLength) 
            throw new CommentTextShouldBeShorterError(_config.TextMaxLength);
            
        return value;
    }
}