using System;
using System.Threading.Tasks;
using Moq;
using TRSale.Domain.Commands.Users;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;
using TRSale.Domain.Services;
using Xunit;

namespace TRSale.Domain.Tests.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task Login()
        {
            var tsc = new TaskCompletionSource<bool>();

            var uow = new Mock<IUnitOfWork>();
            
            var userRepository = new Mock<IUserRepository>();
            var user = new User("John Connor", "john@skynet.com", "123456");
            userRepository.Setup(a => a.FindByEmail("john@skynet.com")).Returns(user);
            

            var userService = new UserService(userRepository.Object, uow.Object);

            var cmd = new LoginCommand();
            cmd.Email = "john@skynet.com";
            cmd.Password = "123456";

            var result = userService.Login(cmd);
            Assert.True(result.Success);

            cmd.Email = "john@skynet.com";
            cmd.Password = "111111";

            result = userService.Login(cmd);
            Assert.False(result.Success);


            cmd.Email = "obama@usa.com";
            cmd.Password = "111111";

            result = userService.Login(cmd);
            Assert.False(result.Success);


            tsc.SetResult(true);
            await tsc.Task;
        }

        [Fact]
        public async Task SignUP()
        {
            var tsc = new TaskCompletionSource<bool>();

            var uow = new Mock<IUnitOfWork>();
            
            var userRepository = new Mock<IUserRepository>();
            var user = new User("John Connor", "john@skynet.com", "123456");
            userRepository.Setup(a => a.FindByEmail("john@skynet.com")).Returns(user);
            

            var userService = new UserService(userRepository.Object, uow.Object);

            var cmd = new SignUpCommand();
            cmd.Email = "terminator@skynet.com";
            cmd.Name = "Model T800";
            cmd.Password = "123456";

            var result = userService.SignUp(cmd);
            Assert.True(result.Success);

            cmd.Email = "john@skynet.com";
            result = userService.SignUp(cmd);
            Assert.False(result.Success);


            cmd.Email = String.Empty;
            result = userService.SignUp(cmd);
            Assert.False(result.Success);


            tsc.SetResult(true);
            await tsc.Task;
        }
        
    }
}