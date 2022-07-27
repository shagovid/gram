using System;
using Rossgram.Application.Common.Interfaces;

namespace Rossgram.WebApi.Services;

public class RandomService : IRandomService
{
    private readonly Random _random = new Random();

    public int Next() => _random.Next();

    public int Next(int maxValue) => _random.Next(maxValue);

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);
    public void NextBytes(byte[] buffer) => _random.NextBytes(buffer);
}