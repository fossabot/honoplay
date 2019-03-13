using System;
using System.Security.Cryptography;
using System.Text;

namespace Honoplay.Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetSHA512(this string value, byte[] salt)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            byte[] saltValue = salt ?? new byte[0];
            var data = Encoding.UTF8.GetBytes(value).Combine(saltValue);
            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(data);
            }
        }

        public static int ToInt(this string value)
        {
            return int.TryParse(value, out int result) ? result : -1;
        }
    }
}