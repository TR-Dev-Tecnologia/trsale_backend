using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Interfaces.Infra;

namespace TRSale.DataBase.Company
{
    public class CompanyEnviroment : ICompanyEnviroment
    {
        public void Create(Guid companyId)
        {
            var connectionString = Environment.GetEnvironmentVariable("trsale_company_db");
            connectionString = connectionString?.Replace("Database=TRSaleCompany;", $"Database=TR{companyId.ToString()};");
            
            var optionsBuilder  = new DbContextOptionsBuilder<TRSaleCompanyContext>();            
            var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 26)); 
            optionsBuilder.UseMySql(connectionString, mysqlServerVersion);

            var context = new TRSaleCompanyContext(optionsBuilder.Options);
            context.Database.Migrate();
        }
    }
}