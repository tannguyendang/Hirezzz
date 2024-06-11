using System.Security.Cryptography;
using System.Text;

namespace Hirezzz;
public static class Helper
{
    public static byte[] Hash(string plaintext)
    {
        HashAlgorithm algorithm = SHA512.Create();
        return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
    }
    public static string HashString(string plaintext)
    {
        // byte[] arr = Hash(plaintext);
        // StringBuilder sb = new StringBuilder();
        // foreach (byte item in arr)
        // {
        //     sb.Append(item.ToString("x2"));
        // }
        // return sb.ToString();
        return Convert.ToHexString(Hash(plaintext));
    }
    public static string RandomString(int len)
    {
        char[] arr = new char[len];
        Random random = new Random();
        string pattern = "qwertyuiopasdfghjklzxcvbnm1234567890";
        for (int i = 0; i < len; i++)
        {
            arr[i] = pattern[random.Next(pattern.Length)];
        }
        return string.Join("", arr);
    }
}