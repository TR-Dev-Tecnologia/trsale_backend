using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Entites;

namespace TRSale.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Detete(Guid id);
        TEntity? Get(Guid id);        
    }
}