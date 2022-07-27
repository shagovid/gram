using System.Linq;
using System.Security.Cryptography;
using Rossgram.Application.Common.Interfaces.Configs;
using Rossgram.Domain.Errors.ValueObjects.Password;
using Rossgram.Domain.ValueObjects;

namespace Rossgram.Application.Common.Services;

public class PasswordController
{
    // DONT CHANGE THIS CONSTANTS!
    private const int SALT_BYTE_SIZE = 24;
    private const int HASH_BYTE_SIZE = 24;
    private const int HASHING_ITERATIONS_COUNT = 10100;
            
    private readonly IPasswordConfig _config;

    public PasswordController(IPasswordConfig config)
    {
        _config = config;
    }

    public Password Create(string? value)
    {
        if (string.IsNullOrEmpty(value)) 
            throw new PasswordShouldHasValueError();
        if (value.Length < _config.MinLength) 
            throw new PasswordShouldBeLongerError(_config.MinLength);

        byte[] salt = GenerateSalt();
        byte[] hash = CreateHash(value, salt);
                
        return new Password(hash, salt);
    }

    public bool Compare(Password encryptedPassword, string? value)
    {
        if (value is null) return false;
        byte[] hash = CreateHash(value, encryptedPassword.Salt);
        return encryptedPassword.Hash.SequenceEqual(hash);
    }
            
    private static byte[] CreateHash(string password, byte[] salt)
    {
        using Rfc2898DeriveBytes hashGenerator = new(password, salt);

        hashGenerator.IterationCount = HASHING_ITERATIONS_COUNT;
        return hashGenerator.GetBytes(HASH_BYTE_SIZE);
    }

    private static byte[] GenerateSalt()
    {
        // TODO: obsolete
        using RNGCryptoServiceProvider saltGenerator = new();

        byte[] salt = new byte[SALT_BYTE_SIZE];
        saltGenerator.GetBytes(salt);
        return salt;
    }
}