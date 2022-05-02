using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Commands.Users
{
    public class SignUpCommand
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public GenericCommandResult Validate()
        {
            
            if (string.IsNullOrEmpty(this.Name))
                return new GenericCommandResult(false, "Name invalid");
            
            if (string.IsNullOrEmpty(this.Email))
                return new GenericCommandResult(false, "E-mail Invalid");

            if (string.IsNullOrEmpty(this.Password))
                return new GenericCommandResult(false, "Password invalid");
            
            return new GenericCommandResult(true, "valid");
        }
    }
}