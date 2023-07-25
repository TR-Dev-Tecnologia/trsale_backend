using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRSale.DataBase.Mappings;
using TRSale.Domain.Company.Entities;

namespace TRSale.DataBase.Company.Mappings
{
    public class CategoryMapping: BaseMapping<Category>, IMapping
    {
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
            entity.Property(a => a.Description).HasColumnType("varchar(255)").IsRequired();
            entity.Property(a => a.CategoryMotherId).HasColumnType("char(36)").IsRequired();
            entity.HasOne(b => b.CategoryMother).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}