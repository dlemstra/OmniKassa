// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/OmniKassa).
// Licensed under the MIT License.

using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace OmniKassa
{
    internal static class Seal
    {
        public static string Create(string data, string secretKey)
        {
            Debug.Assert(!string.IsNullOrEmpty(data));
            Debug.Assert(!string.IsNullOrEmpty(secretKey));

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data + secretKey));

                StringBuilder hex = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    hex.AppendFormat("{0:x2}", b);
                }

                return hex.ToString();
            }
        }
    }
}
