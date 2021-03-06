using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TRSale.Domain.Interfaces.Infra;

namespace TRSale.DataBase
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        #nullable disable
        private readonly TRSaleContext _context;
        private IDbContextTransaction _dbContextTransaction;

        public UnitOfWork(TRSaleContext context)
        {
            _context = context;
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


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (_dbContextTransaction != null)
            {
                _dbContextTransaction.Dispose();
            }
        }
    }
}