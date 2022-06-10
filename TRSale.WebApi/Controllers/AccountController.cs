using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Users;
using TRSale.Domain.Interfaces.Services;

namespace TRSale.WebApi.Controllers
{
    [GenericExceptionFilterAttribute]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SignUp([FromServices] IUserService service, [FromBody] SignUpCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            var result = service.SignUp(cmd);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 201
            });
            return await tsc.Task;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromServices] IUserService service, [FromBody] LoginCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            var result = service.Login(cmd);
            //To do :: Create token jwt using Cokies
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

    }
}