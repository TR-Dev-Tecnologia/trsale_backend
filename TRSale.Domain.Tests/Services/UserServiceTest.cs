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

            var emailService = new Mock<IEmailService>();
            

            var userService = new UserService(userRepository.Object, uow.Object, emailService.Object);

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

            var emailService = new Mock<IEmailService>();
            

            var userService = new UserService(userRepository.Object, uow.Object, emailService.Object);

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


            cmd.Email = "terminator@skeynet.com";
            cmd.Name = String.Empty;
            cmd.Password = "123456";
            result = userService.SignUp(cmd);
            Assert.False(result.Success);


            cmd.Email = "terminator@skeynet.com";
            cmd.Name = "Model T800";
            cmd.Password = String.Empty;
            result = userService.SignUp(cmd);
            Assert.False(result.Success);


            userRepository.Setup(a => a.Create(It.IsAny<User>())).Throws(new ArgumentNullException());

            cmd.Email = "terminator@skeynet.com";
            cmd.Name = "Model T800";
            cmd.Password = "123456";
            
            Assert.Throws<ArgumentNullException>(() => result = userService.SignUp(cmd));
                        
            tsc.SetResult(true);
            await tsc.Task;
        }
        
        [Fact]
        public async Task Forgot_and_Recovery()
        {

            var tsc = new TaskCompletionSource<bool>();

            var uow = new Mock<IUnitOfWork>();
            
            var userRepository = new Mock<IUserRepository>();
            var user = new User("John Connor", "john@skynet.com", "123456");
            userRepository.Setup(a => a.FindByToken(It.IsAny<string>())).Returns(user);
            userRepository.Setup(a => a.FindByEmail("john@skynet.com")).Returns(user);

            var emailService = new Mock<IEmailService>();
            

            var userService = new UserService(userRepository.Object, uow.Object, emailService.Object);
            var cmd = new ForgotCommand();
            cmd.Email = "obama@skynet.com";
            var result = userService.Forgot(cmd);
            Assert.False(result.Success);


            cmd.Email = string.Empty;
            result = userService.Forgot(cmd);
            Assert.False(result.Success);


            cmd.Email = "john@skynet.com";
            result = userService.Forgot(cmd);
            Assert.True(result.Success);

            Assert.NotEmpty(user.PasswordToken);

            var cmdRecovery = new RecoveryPasswordCommand();
    
            result = userService.Recovery(cmdRecovery);
            Assert.False(result.Success);

            cmdRecovery.NewPassword = "112233";
            cmdRecovery.Token = String.Empty;
            result = userService.Recovery(cmdRecovery);
            Assert.False(result.Success);
            

            cmdRecovery.Token = user.PasswordToken!;
            cmdRecovery.NewPassword = "123";
            result = userService.Recovery(cmdRecovery);
            Assert.False(result.Success);

            cmdRecovery.NewPassword = String.Empty;
            result = userService.Recovery(cmdRecovery);
            Assert.False(result.Success);
            
            result = userService.Recovery(cmdRecovery);
            Assert.False(result.Success);


            userRepository.Setup(a => a.FindByToken(user.PasswordToken!)).Returns(user);
            cmdRecovery.Token = "xyz";
            cmdRecovery.NewPassword = "112233";
            result = userService.Recovery(cmdRecovery);
            Assert.True(result.Success);


            cmdRecovery.Token = user.PasswordToken!;
            cmdRecovery.NewPassword = "112233";
            result = userService.Recovery(cmdRecovery);
            Assert.True(result.Success);


            var cmdLogin = new LoginCommand();
            cmdLogin.Email = "john@skynet.com";
            cmdLogin.Password = "112233";
            result = userService.Login(cmdLogin);
            Assert.True(result.Success);

            tsc.SetResult(true);
            await tsc.Task;
        }
    }
}