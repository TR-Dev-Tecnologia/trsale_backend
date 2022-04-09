using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Commands
{
    public class GenericCommandResult
    {
        public GenericCommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public GenericCommandResult(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
 
        
    }
}