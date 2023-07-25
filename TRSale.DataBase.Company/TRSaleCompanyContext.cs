using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRSale.DataBase.Mappings;
using TRSale.Domain.Company.Entities;

namespace TRSale.DataBase.Company
{
    public class TRSaleCompanyContext: DbContext
    {
        public TRSaleCompanyContext(DbContextOptions<TRSaleCompanyContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var typesToMapping = (from x in Assembly.GetExecutingAssembly().GetTypes()
                                  where x.IsClass && typeof(IMapping).IsAssignableFrom(x)
                                  select x).ToList();

            foreach (var mapping in typesToMapping)
            {
                var mappingClass = Activator.CreateInstance(mapping) as IMapping;
                if (mappingClass != null)
                    mappingClass.OnModelCreating(modelBuilder);
            }

        }

        public DbSet<Category> Categories { get; set; } = null!;
 
        
    }
}