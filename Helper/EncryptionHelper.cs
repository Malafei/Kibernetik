using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Kibernetik.Helper
{
    public static class EncryptionHelper
    {
        public static string HashedPassword(string password, string email)
        {
            if (password != null)
            {
                byte[] salt = System.Text.Encoding.UTF8.GetBytes(email);
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
                return hashed;
            }
            return null;
        }


    }
}
