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
        public async Task CriarNovaLojista()
        {
            var tsc = new TaskCompletionSource<bool>();

            var newUser = new User("John Connor", "john@skynet.com", "123456");

            Assert.Equal("John Connor", newUser.Name);
            Assert.Equal("john@skynet.com", newUser.Email);

            Assert.True(newUser.Autenticate("123456"));
            Assert.False(newUser.Autenticate("888888"));

            tsc.SetResult(true);
            await tsc.Task;

        }
        
    }
}