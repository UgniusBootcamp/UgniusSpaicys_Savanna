using System.Security.Cryptography;
using System.Text;

namespace SavannaApp.Data.Helpers.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Method for hashing to sha 256
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>hashed input</returns>
        public static string ToSha256(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
