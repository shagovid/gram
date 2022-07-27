using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rossgram.Domain.Entities;

namespace Rossgram.Infrastructure.Database.Configurations;

public class LogRecordConfiguration : IEntityTypeConfiguration<LogRecord>
{
    public void Configure(EntityTypeBuilder<LogRecord> builder)
    {
            
    }
}