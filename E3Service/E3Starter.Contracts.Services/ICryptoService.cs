using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Contracts.Services;

public interface ICryptoService
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string hash, string salt);
}
