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

        public bool Authenticate(string password)
        {
            return CriptoHelper.VerifyHashedPassword(this.Password, password);            
        }

        public void GenereatePasswordToken()
        {
            this.PasswordToken = Guid.NewGuid().ToString();
            this.PasswordTokenValidity = DateTime.Now.AddMinutes(30);
        }


    }
}