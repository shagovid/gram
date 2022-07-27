using System;
using Rossgram.Application.Common.Interfaces;

namespace Rossgram.WebApi.Services;

public class TimeService : ITimeService
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
}