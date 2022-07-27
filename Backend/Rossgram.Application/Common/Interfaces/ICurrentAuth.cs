using System;
using Rossgram.Domain.Enumerations;

namespace Rossgram.Application.Common.Interfaces;

public interface ICurrentAuth
{
    bool IsAuthorized { get; }
    long Id { get; }
    string Username { get; }
    Role Role { get; }
}