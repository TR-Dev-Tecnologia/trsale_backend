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
            //Usado para Criar as Migrações
            var connectionString = "Server=localhost;Port=3306;Database=TRSale;Uid=root;Pwd=fx870";
            //var connectionString ="Server=db-shopping-do-user-4210498-0.b.db.ondigitalocean.com;Port=25060;Database=B2BPermuta;Uid=b2bPermuta;Pwd=yY5Q85OCNGdiobeV";
                        
            var optionsBuilder = new DbContextOptionsBuilder<TRSaleContext>();
            var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 26)); 
            optionsBuilder.UseMySql(connectionString, mysqlServerVersion);
            return new TRSaleContext(optionsBuilder.Options);
        }
    }
}