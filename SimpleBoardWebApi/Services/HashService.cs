﻿using System;
using System.Security.Cryptography;

namespace SimpleBoardWebApi.Services
{
    public interface IHashService
    {
        string CreateHashFromPassword(string password);
        bool IsPasswordHashedPassword(string password, string hashedPassword);
    }

    public class HashService : IHashService
    {
        private const int Iterations = 10000;

        public string CreateHashFromPassword(string password)
        {
            var salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(20);

            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

        public bool IsPasswordHashedPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
}
