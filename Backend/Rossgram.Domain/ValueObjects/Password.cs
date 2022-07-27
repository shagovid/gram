using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Rossgram.Domain.Errors.ValueObjects.Password;

namespace Rossgram.Domain.ValueObjects;

public class Password : ValueObject
{
    public byte[] Hash { get; private set; }
    public byte[] Salt { get; private set; }

    public Password(byte[] hash, byte[] salt)
    {
        Hash = hash;
        Salt = salt;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Hash;
        yield return Salt;
    }
}