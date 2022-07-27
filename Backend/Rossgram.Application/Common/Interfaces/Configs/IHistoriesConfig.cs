using System;

namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IHistoriesConfig
{
    public TimeSpan TimeToArchive { get; }
    public int MaxCountPerAccount { get; }
}