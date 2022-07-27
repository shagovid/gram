using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Errors.ValueObjects.Nickname;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Common.Services;

public class NicknameController
{
    private readonly IUsernameConfig _config;
    private readonly IAppDbContext _context;

    public NicknameController(
        IUsernameConfig config,
        IAppDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<string> Validate(string? value)
    {
        value = value?.Trim();
        
        if (string.IsNullOrEmpty(value)) 
            throw new NicknameShouldHasValueError();
        if (value.Length < _config.MinLength) 
            throw new NicknameShouldBeLongerError(_config.MinLength);
        if (!value.All(x => x is >= 'a' and <= 'z' || char.IsDigit(x) || _config.AvailableSymbols.Contains(x))) 
            throw new NicknameShouldHasOnlyAvailableSymbolsError(_config.AvailableSymbols);
        if (await _context.Accounts.AnyAsync(x => x.Nickname == value)) 
            throw new NicknameShouldBeUniqueError();
            
        return value;
    }
}