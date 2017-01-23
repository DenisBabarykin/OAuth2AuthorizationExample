using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class HashWorker
    {
        private const string salt = "HelloKitty";

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString + salt));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString + salt))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
