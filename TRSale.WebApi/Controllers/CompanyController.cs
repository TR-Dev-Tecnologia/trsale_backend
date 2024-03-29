using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRSale.Domain.Commands.Companies;
using TRSale.Domain.Interfaces.Repositories;
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
        public async Task<IActionResult> Create([FromServices] ICompanyService service, [FromBody] CreateCompanyCommand cmd)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            

            var userId = HttpContext.User.Claims.Where(a => a.Type == "id").FirstOrDefault()?.Value;
            if (userId != null)
                cmd.UserId = Guid.Parse(userId);
            
            var result = service.Create(cmd);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 201
            });
            return await tsc.Task;
        }

        [HttpGet]  
        [Authorize]      
        public async Task<IActionResult> MyCompanies([FromServices] ICompanyRepository repository)
        {            
            var tsc = new TaskCompletionSource<IActionResult>();
            

            var userId = Guid.Empty;
            var claim = HttpContext.User.Claims.Where(a => a.Type == "id").FirstOrDefault()?.Value;
            if (claim != null)
                userId = Guid.Parse(claim);
            
            var result = repository.MyCompanies(userId);
            tsc.SetResult(new JsonResult(result)
            {
                StatusCode = 200
            });
            return await tsc.Task;
        }

    }
}