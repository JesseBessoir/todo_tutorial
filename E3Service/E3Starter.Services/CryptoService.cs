using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using E3Starter.Contracts.Services;

namespace E3Starter.Services;

public class CryptoService : ICryptoService
{
    private readonly int _saltSize;

    public CryptoService(int saltSize = 32)
    {
        _saltSize = saltSize;
    }

    public string GenerateSalt()
    {
        var salt = new byte[_saltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string salt)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt));
        return Convert.ToBase64String(deriveBytes.GetBytes(256));
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        var hashedProvidedPassword = HashPassword(password, salt);
        return hashedProvidedPassword == hash;
    }
}