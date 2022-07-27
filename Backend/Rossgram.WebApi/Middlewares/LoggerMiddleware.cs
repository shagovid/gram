using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Rossgram.Application.Common.Interfaces;
using Serilog;
using Serilog.Context;

namespace Rossgram.WebApi.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerMiddleware(
        RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext context, ICurrentAuth auth)
    {
        HttpRequest request = context.Request;
        string requestId = Guid.NewGuid().ToString("N");
        ClaimsPrincipal user = context.User;

        using (LogContext.PushProperty("RequestId", requestId))
        {
            // Log Request
            using (LogContext.PushProperty("User", user))
            {
                Log.Information("Processing {RequestMethod} (Path: {Path}) by {UserName}",
                    request.Method,
                    request.Path.Value,
                    auth.IsAuthorized ? auth.Username : "not authorized user");
            }

            if (request.QueryString.HasValue)
                Log.Information("Query {RequestQuery}",
                    request.QueryString.Value);

            Stopwatch timer = Stopwatch.StartNew();
            await this._next(context);
            timer.Stop();

            // Log Response
            Log.Information("Response {ResponseStatus} ({ResponseDuration})",
                context.Response.StatusCode,
                timer.Elapsed);
        }
    }
}