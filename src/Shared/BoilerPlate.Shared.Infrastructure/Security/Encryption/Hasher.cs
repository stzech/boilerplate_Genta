﻿using System.Security.Cryptography;
using System.Text;
using BoilerPlate.Shared.Abstraction.Encryption;

namespace BoilerPlate.Shared.Infrastructure.Security.Encryption;

internal sealed class Hasher : IHasher
{
    public string Hash(string data)
    {
        var bytes = SHA512.HashData(Encoding.UTF8.GetBytes(data));
        var builder = new StringBuilder();
        foreach (var @byte in bytes)
        {
            builder.Append(@byte.ToString("x2"));
        }

        return builder.ToString();
    }
}