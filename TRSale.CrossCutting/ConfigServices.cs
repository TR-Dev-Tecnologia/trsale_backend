using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Services;
using TRSale.Domain.Services;
using TRSale.Infra.Services;

namespace TRSale.CrossCutting
{
    public static class ConfigServices
    {
        public static void Config(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            
        }
    }
}