using System;
using System.Security.Cryptography;



namespace Honoplay.Common.Extensions
{
    public static class ByteArrayExtensions
    {
        public static byte[] Combine(this byte[] firstInput, byte[] secondInput)
        {
            var first = firstInput ?? new byte[0];
            var second = secondInput ?? new byte[0];
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static byte[] GetRandomSalt()
        {
            byte[] buffer;
            using (var rng = new RNGCryptoServiceProvider())
            {
                buffer = new byte[64];
                rng.GetBytes(buffer);
            }
            return buffer;
        }
    }
}