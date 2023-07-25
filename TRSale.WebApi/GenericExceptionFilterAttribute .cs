using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TRSale.Domain.Commands;

namespace TRSale.WebApi
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            while (context.Exception.InnerException != null)
            {
                context.Exception = context.Exception.InnerException;
            }
            
            context.Result = new JsonResult(new GenericCommandResult(false, context.Exception.Message)){StatusCode = 500};
            
        }                    
    }
}