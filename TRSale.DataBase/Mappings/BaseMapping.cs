using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRSale.Domain.Entites;

namespace TRSale.DataBase.Mappings
{
    public abstract class BaseMapping<TEntity> where TEntity : BaseEntity
    {
        protected EntityTypeBuilder<TEntity> entity = null!;

        public virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.entity = modelBuilder.Entity<TEntity>();            
            entity.ToTable(typeof(TEntity).Name).HasCharSet("utf8");
            entity.HasKey(t => t.Id);
            entity.Property(a => a.Id).HasColumnType("char(36)").IsRequired();            
        }
        
    }
}