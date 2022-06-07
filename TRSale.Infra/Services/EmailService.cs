using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TRSale.Domain.Interfaces.Infra;

namespace TRSale.Infra.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string subject, string from, string body)
        {
            
        }
    }
}