using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Helpers;
using Xunit;

namespace TRSale.Domain.Tests.Helpers
{
    public class CriptoHelperTest
    {

        [Fact]
        public async Task CriarNovaLojista()
        {
            var tsc = new TaskCompletionSource<bool>();

            var password = CriptoHelper.HashPassword("123456");
            
            Assert.NotEqual("123456", password);

            Assert.True(CriptoHelper.VerifyHashedPassword(password, "123456"));
            Assert.False(CriptoHelper.VerifyHashedPassword(password, "232323"));
            var key = "482730724F5FDCD34F9414D71D44A28542B81D29929A584BA7791996AE90042A";
            var keyIv = "AA4A196FAB0C7CF44B4B57AA1980FEE3";
            var text = "My secret text";
            var criptText = CriptoHelper.EncryptString(text, key, keyIv);
            Assert.NotEqual(criptText, text);

            criptText = CriptoHelper.DecryptString(criptText, key, keyIv);
            Assert.Equal(criptText, text);


            tsc.SetResult(true);
            await tsc.Task;
        }

        
    }
}