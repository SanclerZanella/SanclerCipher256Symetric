using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SanclerCipher256Symetric
{
    public static class StringCipher
    {

        private const int keySize = 256;

        public static string Encrypt(string message, string passKey)
        {

            var saltStringBytes = GenerateBytes();
            var ivStringBytes = GenerateBytes();
            var messageTextBytes = Encoding.UTF8.GetBytes(message);

            return "";

        }

        private static byte[] GenerateBytes()
        {

            var randomBytes = new byte[32]; // There are 8 bits in a byte, we need 256 bits so we divide 256 by 8 = 32 Bytes. Byte > Bit

            using (var rngCSP = new RNGCryptoServiceProvider())
            {

                rngCSP.GetBytes(randomBytes);

            }

            return randomBytes;

        }

    }
}
