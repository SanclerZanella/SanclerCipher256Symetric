using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SanclerCipher256Symetric
{
    public static class StringCipher
    {

        private const int keySize = 256; // 256 bits

        public static string Encrypt(string message, string passKey)
        {

            var saltStringBytes = GenerateBytes();
            var ivStringBytes = GenerateBytes();
            var messageTextBytes = Encoding.UTF8.GetBytes(message);

            using (var key = new Rfc2898DeriveBytes(passKey, saltStringBytes, 1000))
            {

                var KeyBytes = key.GetBytes(32);
                using (var symetricKey = new RijndaelManaged())
                {

                    symetricKey.BlockSize = keySize;
                    symetricKey.Mode = CipherMode.CBC;
                    symetricKey.Padding = PaddingMode.PKCS7;

                    using (var encryptor = symetricKey.CreateEncryptor(KeyBytes, ivStringBytes))
                    {

                        using (var memory = new MemoryStream())
                        {

                            using (var cryptoStream = new CryptoStream(memory, encryptor, CryptoStreamMode.Write))
                            {

                                cryptoStream.Write(messageTextBytes, 0, messageTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var cipherText = saltStringBytes;
                                cipherText = cipherText.Concat(ivStringBytes).ToArray();
                                cipherText = cipherText.Concat(memory.ToArray()).ToArray();

                                memory.Close();
                                cryptoStream.Close();

                                return Convert.ToBase64String(cipherText);

                            }
                        
                        }

                    }

                }

            }

        }

        public static string Decrypt(string cipherText, string passKey)
        {

            // Get the complete stream of bytes back from cipherText
            // [32 bytes is salt] + [32 bytes is iv] + [n bytes is message bytes]
            var completeStreamOfBytes = Convert.FromBase64String(cipherText);

            // Extract the salt bytes by taking the first 32 bytes from the complete
            // stream of bytes
            var saltBytes = completeStreamOfBytes.Take(32).ToArray();

            // Extract the iv bytes by skiping the first 32 bytes and taking
            // the next 32 bytes from the complete stream of bytes
            var ivBytes = completeStreamOfBytes.Skip(32).Take(32).ToArray();

            // Extract the message bytes by skiping the first 64 bytes and taking
            // the rest from complete stream of bytes
            var msgBytes = completeStreamOfBytes.Skip(64).ToArray();

            using (var key = new Rfc2898DeriveBytes(passKey, saltBytes, 1000))
            {

                var KeyBytes = key.GetBytes(32);
                using (var symmetricKey = new RijndaelManaged())
                {

                    symmetricKey.BlockSize = keySize;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;

                    using (var decryptor = symmetricKey.CreateDecryptor(KeyBytes, ivBytes))
                    {

                        using (var memory = new MemoryStream(msgBytes))
                        {

                            using (var cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Read))
                            {

                                var plainTextBytes = new byte[msgBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                                memory.Close();
                                cryptoStream.Close();

                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                            }

                        }

                    }

                }

            }
                    return "";

        }

        private static byte[] GenerateBytes()
        {

            var randomBytes = new byte[32]; // There are 8 bits in a byte, we need 256 bits so we divide 256 by 8 = 32 Bytes. Byte > Bit

            using (var rngCSP = new RNGCryptoServiceProvider())
            {

                // Fill the randomBytes array with cryptographically secure random bytes
                rngCSP.GetBytes(randomBytes);

            }

            return randomBytes;

        }

    }
}
