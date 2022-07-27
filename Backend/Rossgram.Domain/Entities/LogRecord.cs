using System;
using Serilog.Events;

namespace Rossgram.Domain.Entities;

public class LogRecord : Entity
{
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public LogEventLevel Level { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string? Exception { get; set; }
    public string Properties { get; set; }
        
    #region EF Core constructor
#pragma warning disable 8618
    protected LogRecord(long  id) : base(id) { }
#pragma warning restore 8618
    #endregion
        
    public LogRecord(
        long  id,
        string message,
        string messageTemplate,
        LogEventLevel level,
        DateTimeOffset timeStamp,
        string? exception,
        string properties)
        : base(id)
    {
        Message = message;
        MessageTemplate = messageTemplate;
        Level = level;
        TimeStamp = timeStamp;
        Exception = exception;
        Properties = properties;
    }
}