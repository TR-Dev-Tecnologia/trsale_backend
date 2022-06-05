using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Interfaces.Infra
{
    public interface IEmailService
    {
        void SendEmail(string subject, string from, string body);
    }
}