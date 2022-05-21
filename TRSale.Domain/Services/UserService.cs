using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Users;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;
using TRSale.Domain.Interfaces.Services;

namespace TRSale.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public UserService(IUserRepository userRepository, IUnitOfWork uow)
        {
            _userRepository = userRepository;
            _uow = uow;
        }

        public GenericCommandResult Login(LoginCommand cmd)
        {
            var user = _userRepository.FindByEmail(cmd.Email);
            if (user == null)
                return new GenericCommandResult(false, "E-Mail or Password invalid");

            if (user.Authenticate(cmd.Password)){
                _userRepository.Update(user);
                return new GenericCommandResult(true, "Authenticate Success", new {Name=user.Name, Email=user.Email});
            } else {
                return new GenericCommandResult(false, "E-Mail or Password invalid");
            }
        }

        public GenericCommandResult SignUp(SignUpCommand cmd)        
        {            
            var valid = cmd.Validate();
            if (!valid.Success)
                return valid;

            if (_userRepository.FindByEmail(cmd.Email) != null)
                return new GenericCommandResult(false, "E-Mail exists, try login");
            
            _uow.BeginTransaction();
            try
            {
                var user = new User(cmd.Name, cmd.Email, cmd.Password);
                _userRepository.Create(user);
                _uow.Commit();
            }
            catch(Exception)
            {
                _uow.Rollback();
                throw;
            }
            return new GenericCommandResult(true, "Account created with success");
            
        }
    }
}