using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Entites;

namespace TRSale.DataBase.Mappings
{
    public class UserMapping: BaseMapping<User>, IMapping
    {
        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            entity.Property(a => a.Email).HasColumnType("varchar(255)").IsRequired();
            entity.Property(a => a.Name).HasColumnType("varchar(255)").IsRequired();
            entity.Property(a => a.Password).HasColumnType("longtext").IsRequired();
            entity.Property(a => a.LastAccess).HasColumnType("datetime");

            entity.Property(a => a.PasswordToken).HasColumnType("varchar(255)");
            entity.Property(a => a.PasswordTokenValidity).HasColumnType("datetime");            

            entity.HasIndex(a => a.Email).HasDatabaseName("UnqUserEmail").IsUnique();
        }
        
    }
}