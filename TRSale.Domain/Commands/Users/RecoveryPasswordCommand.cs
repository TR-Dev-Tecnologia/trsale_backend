using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Commands.Users
{
    public class RecoveryPasswordCommand
    {
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; } = null!;

        public GenericCommandResult Validate()
        {
                        
            if (string.IsNullOrEmpty(this.Token))
                return new GenericCommandResult(false, "Token Invalid");

            if (string.IsNullOrEmpty(this.NewPassword))
                return new GenericCommandResult(false, "Password Invalid");

            if (this.NewPassword.Length < 6)
                return new GenericCommandResult(false, "Password Invalid");
                
            return new GenericCommandResult(true, "valid");
        }
    }
}