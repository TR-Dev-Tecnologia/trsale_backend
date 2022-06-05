using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Commands.Users
{
    public class ForgotCommand
    {
        public string Email { get; set; } = null!;


        public GenericCommandResult Validate()
        {
                        
            if (string.IsNullOrEmpty(this.Email))
                return new GenericCommandResult(false, "E-mail Invalid");
                
            return new GenericCommandResult(true, "valid");
        }
    }
}