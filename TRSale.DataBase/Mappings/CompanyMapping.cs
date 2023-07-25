using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Entites;

namespace TRSale.DataBase.Mappings
{
    public class CompanyMapping: BaseMapping<Company>, IMapping
    {
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            entity.Property(a => a.Name).HasColumnType("varchar(255)").IsRequired();
        }
                
        
    }
}