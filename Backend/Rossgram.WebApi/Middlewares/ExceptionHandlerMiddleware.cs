using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Rossgram.Domain.Errors.Access;
using Rossgram.WebApi.DataTransfers;
using Serilog;

namespace Rossgram.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly MvcNewtonsoftJsonOptions _jsonOptions;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
    {
        this._next = next;
        _jsonOptions = jsonOptions.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "Error in request");
            await WriteResponseWithException(context, exception);
        }
    }

    private async Task WriteResponseWithException(HttpContext context, Exception exception)
    {
        IEnumerable<ExceptionDTO> exceptionDTOs = exception is AggregateException aggregate
            ? aggregate.InnerExceptions.Select(ExceptionDTO.CreateFromException)
            : new [] { ExceptionDTO.CreateFromException(exception) };

        string result = JsonConvert.SerializeObject(exceptionDTOs, _jsonOptions.SerializerSettings);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            UnauthorizedAccessError => 401,
            ForbiddenAccessError => 403,
            _ => (int) HttpStatusCode.InternalServerError
        };
        await context.Response.WriteAsync(result);
    }
}