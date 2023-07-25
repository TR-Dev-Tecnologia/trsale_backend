using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRSale.Domain.Helpers;
using Xunit;

namespace TRSale.Domain.Tests.Helpers
{
    public class CriptoHelperTest
    {

        [Fact]
        public async Task Cripto()
        {
            var tsc = new TaskCompletionSource<bool>();

            var password = CriptoHelper.HashPassword("123456");
            
            Assert.NotEqual("123456", password);

            Assert.True(CriptoHelper.VerifyHashedPassword(password, "123456"));
            Assert.False(CriptoHelper.VerifyHashedPassword(password, "232323"));

            Assert.False(CriptoHelper.VerifyHashedPassword(null, "232323"));

            Assert.Throws<ArgumentNullException>(() => CriptoHelper.VerifyHashedPassword(password, null));

            Assert.Throws<ArgumentNullException>(() => CriptoHelper.HashPassword(null));
            

     
            byte[] x1 = Encoding.UTF8.GetBytes("5baa6413");
            byte[] x2 = Encoding.UTF8.GetBytes("z");
            Assert.False(CriptoHelper.ByteArraysEqual(x1, x2));

            x1 = Encoding.UTF8.GetBytes("5baa6413");
            x2 = Encoding.UTF8.GetBytes("5baa6413");
            Assert.True(CriptoHelper.ByteArraysEqual(x1, x2));

            x1 = Encoding.UTF8.GetBytes("5baa6413");
            x2 = Encoding.UTF8.GetBytes("5baa5566");
            Assert.False(CriptoHelper.ByteArraysEqual(x1, x2));

            tsc.SetResult(true);
            await tsc.Task;
        }

        
    }
}