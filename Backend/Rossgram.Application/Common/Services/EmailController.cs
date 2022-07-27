using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Errors.ValueObjects.Email;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Common.Services;

public class EmailController
{
    private readonly IEmailConfig _config;
    private readonly IAppDbContext _context;

    public EmailController(
        IEmailConfig config,
        IAppDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<string> Validate(string? value)
    {
        value = value?.Trim();
        
        if (string.IsNullOrEmpty(value)) 
            throw new EmailShouldHasValueError();
        if (!_config.Pattern.IsMatch(value)) 
            throw new EmailShouldBeValidError();
        if (await _context.Accounts.AnyAsync(x => x.Email == value)) 
            throw new EmailShouldBeUniqueError();
                
        return value;
    }
}