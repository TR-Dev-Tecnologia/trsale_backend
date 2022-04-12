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
                throw new ArgumentNullException("Error please check psw");
            }                     
      
            
            byte[] salt = new Rfc2898DeriveBytes(password, 16).Salt;
            byte[] buffer2;
   
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string? hashedPassword, string? password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("Psw error");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
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