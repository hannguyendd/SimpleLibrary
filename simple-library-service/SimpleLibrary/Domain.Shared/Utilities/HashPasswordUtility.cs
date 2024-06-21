using System.Security.Cryptography;
using System.Text;

namespace SimpleLibrary.Domain.Shared.Utilities;

public static class HashPasswordUtility
{
    public static void HashPassword(string plainPassword, out byte[] hashedPass, out byte[] salt)
    {
        using var hash = new HMACSHA512();
        hashedPass = hash.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
        salt = hash.Key;
    }

    public static bool IsValidPassword(string plainPass, byte[] hashPass, byte[] salt)
    {
        using var hash = new HMACSHA512(salt);
        var newPassHash = hash.ComputeHash(Encoding.UTF8.GetBytes(plainPass));

        return ByteArrayComparer.IsEqual(newPassHash, hashPass);
    }
}