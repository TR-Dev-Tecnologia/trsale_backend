using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TRSale.DataBase
{
    public class ContextFactory : IDesignTimeDbContextFactory<TRSaleContext>
    {
        public TRSaleContext CreateDbContext(string[] args)
        {            
            
            var connectionString = Environment.GetEnvironmentVariable("trsaledb");
            
                        
            var optionsBuilder = new DbContextOptionsBuilder<TRSaleContext>();
            var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 26)); 
            optionsBuilder.UseMySql(connectionString, mysqlServerVersion);
            return new TRSaleContext(optionsBuilder.Options);
        }
    }
}