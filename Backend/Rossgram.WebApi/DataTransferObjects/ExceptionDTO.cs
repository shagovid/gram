using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rossgram.Domain.Enumerations;
using Rossgram.Domain.Errors;

namespace Rossgram.WebApi.DataTransfers;

public class ExceptionDTO
{
    public string? Message { get; set; }
    public string? ExceptionType { get; set; }
    public List<ExceptionPropertyDTO>? Parameters { get; set; }
    public ExceptionDTO? InnerException { get; set; }
    public string? StackTrace { get; set; }
    
    public static ExceptionDTO CreateFromException(Exception exception)
    {
        // For general exceptions
        if (exception is not Error error)
        {
            return new ExceptionDTO()
            {
#if DEBUG
                Message = exception.Message,
                ExceptionType = GetExceptionName(exception),
                Parameters = new List<ExceptionPropertyDTO>(),
                InnerException = exception.InnerException is not null
                    ? CreateFromException(exception.InnerException)
                    : null,
                StackTrace = exception.StackTrace
#else
                Message = "Some error",
                ExceptionType = "SomeError"
#endif
            };
        }

        // Loading all properties
        List<PropertyInfo> properties = new();
        Type? currentType = error.GetType();
        while (currentType is not null && currentType.IsSubclassOf(typeof(Error)))
        {
            properties.AddRange(currentType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly));

            currentType = currentType.BaseType;
        }

        // Mapping properties
        List<ExceptionPropertyDTO> parameters = properties
            .Select(property =>
            {
                object? propertyValue = property.GetValue(error);

                return new ExceptionPropertyDTO()
                {
                    Name = property.Name,
                    Type = property.PropertyType.Name,
                    Value = propertyValue switch
                    {
                        Enumeration asEnumeration => asEnumeration.Code,
                        Type asType => asType.Name,
                        _ => propertyValue
                    }
                };
            })
            .Where(x => x.Value is not null)
            .ToList();

        return new ExceptionDTO()
        {
            Message = exception.Message,
            ExceptionType = GetExceptionName(exception),
            Parameters = parameters,
            InnerException = exception.InnerException is not null
                ? CreateFromException(exception.InnerException)
                : null,
#if DEBUG
            StackTrace = exception.StackTrace
#endif
        };
    }

    private static string GetExceptionName(Exception e)
    {
        string result = e.GetType().Name;
        // Remove part from generic arguments
        result = string.Concat(result.TakeWhile(x => x != '`'));
        return result;
    }
}