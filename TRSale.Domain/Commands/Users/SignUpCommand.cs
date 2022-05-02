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
    }
}