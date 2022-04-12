using System.Security.Cryptography;
using System.Text;

namespace TRSale.Domain.Helpers
{
    public static class CriptoHelper
    {
        public static bool ByteArraysEqual(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }

        public static string HashPassword(string? password)
        {  
            if (password == null)
            {
                throw new ArgumentNullException("password is null");
            }                     
      
            
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(builder.ToString()));  
            }
        }

        public static bool VerifyHashedPassword(string? hashedPassword, string? password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("Psw error");
            }

            return CriptoHelper.HashPassword(password) == hashedPassword;
        }

        public static string EncryptString(string plainText, string key, string keyIv)
        {
            int keyByteSize = 32;
            byte[] keyBytes = new byte[keyByteSize];
            byte[] ivBytes = new byte[16];
            byte[] cipherBytes;


            int len = keyByteSize * 2;
            for (int i = 0; i < len; i += 2)
            {
                keyBytes[i / 2] = Convert.ToByte(key.Substring(i, 2), 16);
                if (i < 32)
                    ivBytes[i / 2] = Convert.ToByte(keyIv.Substring(i, 2), 16);
            }


            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                cipherBytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(cipherBytes);
        }
        public static string DecryptString(string cipherText, string key, string keyIv)
        {
            try
            {
                int keyByteSize = 32;                
                byte[] keyBytes = new byte[keyByteSize];
                byte[] ivBytes = new byte[16];
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                int len = keyByteSize * 2;
                for (int i = 0; i < len; i += 2)
                {
                    keyBytes[i / 2] = Convert.ToByte(key.Substring(i, 2), 16);
                    if (i < 32)
                        ivBytes[i / 2] = Convert.ToByte(keyIv.Substring(i, 2), 16);
                }
                
                using Aes aes = Aes.Create();
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new MemoryStream(cipherBytes);
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader(cryptoStream);

                return streamReader.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }


        }
    }
}