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
            var connectionString = "Server=localhost;Port=3306;Database=TRSale;Uid=root;Pwd=fx870";
            
                        
            var optionsBuilder = new DbContextOptionsBuilder<TRSaleContext>();
            var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 26)); 
            optionsBuilder.UseMySql(connectionString, mysqlServerVersion);
            return new TRSaleContext(optionsBuilder.Options);
        }
    }
}