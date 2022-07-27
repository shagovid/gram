using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using NpgsqlTypes;
using Rossgram.Application.Common.Interfaces;
using Rossgram.Domain.Entities;
using Rossgram.WebApi.Middlewares;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;

namespace Rossgram.WebApi.Configurations;

public static partial class AppConfiguration
{
    public static Logger CreateLogger(string connectionString)
    {
        return new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.PostgreSQL(
                connectionString: connectionString,
                tableName: nameof(IAppDbContext.Logs),
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
                    {nameof(LogRecord.TimeStamp), new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                    {nameof(LogRecord.Level), new LevelColumnWriter(false, NpgsqlDbType.Integer) },
                    {nameof(LogRecord.Message), new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                    {nameof(LogRecord.MessageTemplate), new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                    {nameof(LogRecord.Exception), new ExceptionColumnWriter(NpgsqlDbType.Text) },
                    {nameof(LogRecord.Properties), new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                },
                period: TimeSpan.FromSeconds(5),
                useCopy: false,
                respectCase: true)
            .Enrich.FromLogContext()
            .CreateLogger();
    }

    public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggerMiddleware>();
        return app;
    }
}