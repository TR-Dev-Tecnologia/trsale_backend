using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TRSale.DataBase.Company
{
    public class ContextFactory: IDesignTimeDbContextFactory<TRSaleCompanyContext>
    {
        public TRSaleCompanyContext CreateDbContext(string[] args)
        {                        
            //var connectionString = Environment.GetEnvironmentVariable("trsale_company_db");
            var connectionString   = "Server=localhost;Port=3306;Database=TRSaleCompany;Uid=root;Pwd=fx870";
            var optionsBuilder     = new DbContextOptionsBuilder<TRSaleCompanyContext>();
            var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 26)); 
            optionsBuilder.UseMySql(connectionString, mysqlServerVersion);
            return new TRSaleCompanyContext(optionsBuilder.Options);
        }
    }
}