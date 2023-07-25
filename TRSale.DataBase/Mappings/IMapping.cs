using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TRSale.DataBase.Mappings
{
    public interface IMapping
    {
        void OnModelCreating(ModelBuilder modelBuilder);
        
    }
}