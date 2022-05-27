using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace TRSale.DataBase
{
    public class UnitOfWork: IDisposable
    {
        #nullable disable
        private readonly TRSaleContext _context;
        private IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(TRSaleContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.DisposeAsync();
            }

        }

        public void BeginTransaction()
        {
            _dbContextTransaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_dbContextTransaction != null)
            {        
                _context.SaveChangesAsync().Wait();
                _dbContextTransaction.CommitAsync().Wait();
                this.DetachedChanges();
                _dbContextTransaction = null;
            }            
        }

        public void DetachedChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {                
                if (entry.State == EntityState.Unchanged)
                {
                    entry.State = EntityState.Detached;
                }
            }
        }
        public void Rollback()
        {
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.RollbackAsync().Wait();
            }
        }

        public IDbTransaction CurrentTransaction()
        {
            IDbTransaction result = null;
            if (_dbContextTransaction != null)
            {
                result = _dbContextTransaction.GetDbTransaction();
            }
            return result;
        }
        
    }
}