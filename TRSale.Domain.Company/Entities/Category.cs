using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Entites;

namespace TRSale.Domain.Company.Entities
{
    public class Category: BaseEntity
    {
        public Category(string description, Guid? categoryMotherId)
        {
            if (String.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            Description = description;
            CategoryMotherId = categoryMotherId;
        }

        private Category()
        {
            
        }
        public string Description { get; private set; } = null!;
        public Guid? CategoryMotherId { get; private set; }
        public Category CategoryMother { get; private set; } = null!;

        public void Update(string description, Guid? categoryMotherId)
        {
            if (String.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
                
            this.Description = description;
            this.CategoryMotherId = categoryMotherId;
        }
    }
}