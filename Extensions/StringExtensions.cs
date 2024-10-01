using System.Security.Cryptography;
using System.Text;

namespace QueryDeveloper_WPF.Extensions
{
    public static class StringExtensions
    {
        public static string ToSHA256String(this string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
