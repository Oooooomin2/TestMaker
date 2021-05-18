using System;
using System.Security.Cryptography;

namespace DDD.Domain.Helper
{
    public static class Password
    {
        public static readonly int saltSize = 32;
        public static readonly int hashSize = 32;
        public static readonly int iteration = 10000;

        private static byte[] CreateSalt(int size)
        {
            var bytes = new byte[size];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(bytes);
            }
            return bytes;
        }

        private static byte[] CreatePBKDF2Hash(string password, byte[] salt, int size, int iteration)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, iteration);
            return rfc2898DeriveBytes.GetBytes(size);
        }

        public static string CreateHashTextBase64(string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var inputHashBytes = CreatePBKDF2Hash(password, saltBytes, hashSize, iteration);
            return Convert.ToBase64String(inputHashBytes);
        }
        public static string CreateSaltBase64()
        {
            return Convert.ToBase64String(CreateSalt(saltSize));
        }

        public static string CreatePasswordHashBase64(byte[] saltBytes, string password)
        {
            return Convert.ToBase64String(CreatePBKDF2Hash(password, saltBytes, hashSize, iteration));
        }
    }
}
