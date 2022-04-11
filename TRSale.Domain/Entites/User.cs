using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Helpers;

namespace TRSale.Domain.Entites
{
    public class User: BaseEntity
    {
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = CriptoHelper.HashPassword(password);
        }

        public string Name { get; private set; }        
        public string Email { get; private set; }
        public string Password { get; private set; }       

        public string? PasswordToken { get; private set; } = null!;

        public DateTime? PasswordTokenValidity { get; private set; }

        public DateTime? LastAccess { get; private set; }

        public bool Authenticate(string password)
        {
            var result = CriptoHelper.VerifyHashedPassword(this.Password, password);            
            if (result){
                this.LastAccess = DateTime.Now;
            }
            return result;
        }

        public void GenereatePasswordToken()
        {
            this.PasswordToken = Guid.NewGuid().ToString();
            this.PasswordTokenValidity = DateTime.Now.AddMinutes(30);
        }

        public void UpdatePassword(string token, string newPassword)
        {
            if (this.PasswordToken != token)
                throw new ArgumentException("Token invalid");
            
            if (this.PasswordTokenValidity < DateTime.Now)
                throw new ArgumentException("Token invalid");

            this.Password = CriptoHelper.HashPassword(newPassword);
            this.PasswordToken = null;
            this.PasswordTokenValidity = null;
            //teste in the end file
        }


    }
}