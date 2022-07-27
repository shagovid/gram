using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Rossgram.Infrastructure.Database;

namespace Rossgram.Infrastructure;

public static class Extensions
{
    public static async Task EnsureCreateViewAsync(this DatabaseFacade database, 
        DatabaseView viewConfiguration)
    {
        // Drop view if exists
        await database.ExecuteSqlRawAsync(viewConfiguration.GetDropSql());
            
        // Create view
        await database.ExecuteSqlRawAsync(viewConfiguration.GetCreateSql());
    }
}