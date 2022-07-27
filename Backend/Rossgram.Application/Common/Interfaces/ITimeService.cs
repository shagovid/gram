using System;

namespace Rossgram.Application.Common.Interfaces;

public interface ITimeService
{
    DateTimeOffset Now { get; }
}