using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Models.Protection
{
    public class AES : MonoBehaviour
    {
        
        private const int KeyLength = 128;
        private const string SaltKey = "9R4a}DDJ~#pe2goW";

        public static string Encrypt(byte[] value, string password, string ivKey)
        {
            var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes(ivKey));

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            
            cryptoStream.Write(value, 0, value.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            memoryStream.Close();

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string Encrypt(string value, string password, string ivKey)
        {
            return Encrypt(Encoding.UTF8.GetBytes(value), password, ivKey);
        }

        public static string Decrypt(string value, string password, string ivKey)
        {
            var cipherTextBytes = Convert.FromBase64String(value);
            var keyBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(SaltKey)).GetBytes(KeyLength / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None };
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes(ivKey));

            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
        
    }
}
