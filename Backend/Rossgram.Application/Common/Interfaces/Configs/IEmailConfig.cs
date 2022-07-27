using System.Text.RegularExpressions;

namespace Rossgram.Application.Common.Interfaces.Configs;

public interface IEmailConfig
{
    public Regex Pattern { get; }
}