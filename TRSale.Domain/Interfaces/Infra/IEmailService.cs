using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Interfaces.Infra
{
    public interface IEmailService
    {
        void Send(string subject, string from, string body);
    }
}