using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TRSale.DataBase.Mappings;
using TRSale.Domain.Entites;

namespace TRSale.DataBase
{
    public class TRSaleContext: DbContext
    {
        public TRSaleContext(DbContextOptions<TRSaleContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region FKs adjust
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

                if (relationship.Properties.Count != 0)
                {
                    var prop = relationship.Properties[0];

                    var PTR = prop.PropertyInfo;
                    if (PTR != null)
                    {
                        var attributes = PTR.CustomAttributes;
                        foreach (var attribute in attributes)
                        {
                            if (attribute.AttributeType.Name.ToLower() == "cascade")
                            {
                                relationship.DeleteBehavior = DeleteBehavior.Cascade;
                            }
                        }
                    }
                }

            }
            #endregion            

            //Dynamic Mapping by Reflection
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

        public DbSet<User> Users { get; set; } = null!;
                
    }
}