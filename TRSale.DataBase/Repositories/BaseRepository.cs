using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using TRSale.Domain.Attributes;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;

namespace TRSale.DataBase.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected TRSaleContext _context;

        protected DbSet<TEntity> _dbSet;

        protected DbConnection _connection;

        protected IUnitOfWork _uow;

        public BaseRepository(TRSaleContext context, IUnitOfWork uow)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _connection = _context.Database.GetDbConnection();
            _uow = uow;            
        }

        protected void ApplyConfig<T>(T model)
        {
            var properties = model!.GetType().GetProperties();
            foreach (var prop in properties)
            {

                var attributies = prop.GetCustomAttributes(true);
                foreach (var attribute in attributies)
                {
                    if (attribute is NotPersist)
                    {
                        prop.SetValue(model, null);
                    }
                }
            }

        }

        public void Create(TEntity entity)
        {
            this.ApplyConfig<TEntity>(entity);
            _dbSet.Add(entity);
        }

        public void Detete(Guid id)
        {
            var model = this.Get(id);
            if (model != null)
            {
                this.ApplyConfig<TEntity>(model);
                _dbSet.Remove(model);
            }
        }

        public TEntity? Get(Guid id)
        {
            return _connection.Query<TEntity>($@"Select * from {typeof(TEntity).Name} where id = @Id", new {Id = id}, _uow.CurrentTransaction()).FirstOrDefault();
        }

        public void Update(TEntity entity)
        {
            this.ApplyConfig<TEntity>(entity);
            _dbSet.Update(entity);
        }
    }
}