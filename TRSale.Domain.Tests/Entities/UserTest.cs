using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Entites;
using Xunit;

namespace TRSale.Domain.Tests.Entities
{
    public class UserTest
    {
        [Fact]
        public async Task NewUser()
        {
            var tsc = new TaskCompletionSource<bool>();

            var newUser = new User("John Connor", "john@skynet.com", "123456");

            Assert.Equal("John Connor", newUser.Name);
            Assert.Equal("john@skynet.com", newUser.Email);

            Assert.True(newUser.Authenticate("123456"));

            Assert.False(newUser.Authenticate("888888"));

            tsc.SetResult(true);
            await tsc.Task;

        }


        [Fact]
        public async Task UpatePassword()
        {
            var tsc = new TaskCompletionSource<bool>();

            var newUser = new User("John Connor", "john@skynet.com", "123456");

            Assert.True(newUser.Authenticate("123456"));

            newUser.GenereatePasswordToken();
            
            newUser.UpdatePassword(newUser.PasswordToken!, "888888");

            Assert.True(newUser.Authenticate("888888"));

            tsc.SetResult(true);
            await tsc.Task;

        }
        
    }
}