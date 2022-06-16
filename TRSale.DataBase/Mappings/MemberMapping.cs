using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Entites;

namespace TRSale.DataBase.Mappings
{
    public class MemberMapping: BaseMapping<Member>, IMapping
    {
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            entity.Property(a => a.UserId).HasColumnType("char(36)").IsRequired();  
            entity.Property(a => a.CompanyId).HasColumnType("char(36)").IsRequired();  
            entity.HasIndex(a => new {a.CompanyId, a.UserId}).IsUnique();

            entity.HasOne(b => b.Company).WithOne().OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(b => b.User).WithOne().OnDelete(DeleteBehavior.Restrict);
            
        }
                
        
    }
}