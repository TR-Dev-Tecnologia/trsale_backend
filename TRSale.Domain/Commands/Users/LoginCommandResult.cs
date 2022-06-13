using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Entites;

namespace TRSale.Domain.Commands.Users
{
    public class AuthenticatedData
    {
        public AuthenticatedData()
        {
            this.Id = Guid.Empty;
            this.Email = String.Empty;
            this.Name = String.Empty;
        }
        public AuthenticatedData(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.Name = user.Name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class LoginCommandResult : BaseCommandResult<AuthenticatedData>
    {
        public LoginCommandResult(bool success, string message) : base(success, message, new AuthenticatedData())
        {
        }

        public LoginCommandResult(bool success, string message, User user) : base(success, message, new AuthenticatedData(user))
        {
        }
    }
}