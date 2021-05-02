using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TestMaker.Models
{
    public static class Password
    {
        public static readonly int saltSize = 32;
        public static readonly int hashSize = 32;
        public static readonly int iteration = 10000;
        public static byte[] CreateSalt(int size)
        {
            var bytes = new byte[size];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(bytes);
            }
            return bytes;
        }

        public static byte[] CreatePBKDF2Hash(string password, byte[] salt, int size, int iteration)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iteration))
            {
                return rfc2898DeriveBytes.GetBytes(size);
            }
        }

        public static string ChangeToBase64(byte[] b)
        {
            return Convert.ToBase64String(b);
        }

        public static byte[] ChangeFromBase64(string s)
        {
            return Convert.FromBase64String(s);
        }
    }
}
