using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Entites
{
    public class Company: BaseEntity
    {
        private Company(): base()
        {
            
        }
        
        public string Name { get; private set; } = null!;

        public Company(string name)
        {   if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name is mandatory");

            Name = name;
        }
    }
}