using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Users;

namespace TRSale.Domain.Interfaces.Services
{
    public interface IUserService
    {
        LoginCommandResult Login(LoginCommand cmd);
        GenericCommandResult SignUp(SignUpCommand cmd);
        GenericCommandResult Forgot(ForgotCommand cmd);
        GenericCommandResult Recovery(RecoveryPasswordCommand cmd);
    }
}