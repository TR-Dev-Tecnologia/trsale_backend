using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TRSale.DataBase;
using TRSale.DataBase.Company;
using TRSale.DataBase.Repositories;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;

namespace TRSale.CrossCutting
{
    public static class ConfigRepositories
    {
        public static void Config(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("trsale_db");

            services.AddDbContext<TRSaleContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26))));

            services.AddScoped<TRSaleContext, TRSaleContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBaseRepository<Member>, BaseRepository<Member>>();
            services.AddScoped<ICompanyEnviroment, CompanyEnviroment>();
                 
            
        }
        
    }
}