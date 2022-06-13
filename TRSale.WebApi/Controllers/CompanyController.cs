using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRSale.Domain.Commands.Companies;
using TRSale.Domain.Interfaces.Services;
using TRSale.WebApi.Services;

namespace TRSale.WebApi.Controllers
{
    [Route("[controller]")]
    [GenericExceptionFilterAttribute]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        [HttpPost]  
        [Authorize]      
        public async Task<IActionResult> SignUp([FromServices] ICompanyService service, [FromBody] CreateCompanyCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            
            cmd.UserId = Guid.Parse(User.Identity.Name);
            
            var result = service.Create(cmd);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 201
            });
            return await tsc.Task;
        }

    }
}