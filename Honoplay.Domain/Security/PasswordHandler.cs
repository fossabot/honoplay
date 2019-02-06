using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Honoplay.Domain.Security
{
    public static class Hash
    {
        public static byte[] GetSHA512(string value, byte[] salt)
        {
            var data = Encoding.UTF8.GetBytes(value).Combine(salt);
            using (SHA512 shaM = new SHA512Managed())
            {
                return shaM.ComputeHash(data);
            }
        }
    }
}
