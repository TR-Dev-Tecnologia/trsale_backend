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
                throw new ArgumentNullException(nameof(password));
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
                throw new ArgumentNullException(nameof(password));
            }

            return CriptoHelper.HashPassword(password) == hashedPassword;
        }
        
    }
}