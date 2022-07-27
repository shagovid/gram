using System;

namespace Rossgram.Application.Common.Interfaces;

public interface IRandomService
{
    public int Next();
    public int Next(int maxValue);
    public int Next(int minValue, int maxValue);
    public void NextBytes(byte[] buffer);

    public string NextString()
    {
        byte[] randomBytes = new byte[16];
        NextBytes(randomBytes);
        string randomText = new Guid(randomBytes).ToString("N");
        return randomText;
    }
}