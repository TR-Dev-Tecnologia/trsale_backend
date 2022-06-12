using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Users;
using TRSale.Domain.Interfaces.Services;
using TRSale.WebApi.Services;

namespace TRSale.WebApi.Controllers
{
    [GenericExceptionFilterAttribute]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("SignUp")]        
        [AllowAnonymous]
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
            
            if (result.Success)
            {                          
                var token = TokenService.GenerateToken(result.Data!.Id.ToString(), result.Data.Name, result.Data.Email);
                string? domainCookie = Environment.GetEnvironmentVariable("DomainCookie");
                Response.Cookies.Append("tk_TRSale", token, new CookieOptions() {
                    Domain = domainCookie,
                    HttpOnly = true, 
                    SameSite = 
                    SameSiteMode.Strict, 
                    Secure = true,
                    MaxAge = TimeSpan.FromHours(2) 
                });
            }
                

            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

        [HttpPost]
        [Route("Logout")]        
        [Authorize]
        public async Task<IActionResult> Logout()
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            Response.Cookies.Delete("tk_TRSale");
            tsc.SetResult(new JsonResult(new GenericCommandResult(true, "Successes"))
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }


        [HttpPost]
        [Route("forgot")]
        [AllowAnonymous]
        public async Task<IActionResult> Forgot([FromServices] IUserService service, [FromBody] ForgotCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            var result = service.Forgot(cmd);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

        [HttpPost]
        [Route("recovery")]
        [AllowAnonymous]
        public async Task<IActionResult> Recovery([FromServices] IUserService service, [FromBody] RecoveryPasswordCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            var result = service.Recovery(cmd);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

        [HttpGet]
        [Authorize]  
        public async Task<IActionResult> Get([FromServices] IUserService service)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            
            tsc.SetResult(new JsonResult(new {TESTE = "OK"})
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

    }
}