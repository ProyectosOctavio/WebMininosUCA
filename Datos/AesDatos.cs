using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Datos
{
    public class AesDatos
    {
        private static Encoding encoding = Encoding.UTF8;
        private static readonly string ALPHA_NUMERIC_STRING = "ABCDEFGHIHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.$@#/*-+?_&";
        private static Aes myAes = Aes.Create();

        public string randomAlphaNumeric(int count)
        {
            StringBuilder builder = new StringBuilder();
            Random rnd = new Random();
            while (count-- != 0)
            {
                int character = (int)(rnd.Next(ALPHA_NUMERIC_STRING.Length));
                builder.Append(ALPHA_NUMERIC_STRING.ToCharArray()[character]);
            }

            return builder.ToString();
        }

        public string Encrypt_Aes(string plainText, string Key, string IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;

            myAes.Key = encoding.GetBytes(Key);
            ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, Convert.FromBase64String(IV));

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt_Aes(string cipherText, string Key, string IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            string plainText = null;
            myAes.Key = encoding.GetBytes(Key);
            ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, Convert.FromBase64String(IV));

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plainText = srDecrypt.ReadToEnd();
                    }
                }
            }

            return plainText;
        }
    }
}