using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;

namespace TRSale.DataBase.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(TRSaleContext context, IUnitOfWork uow) : base(context, uow)
        {
            
        }
        public IEnumerable<Company> MyCompanies(Guid userId)
        {
            return _connection.Query<Company>($@"Select a.* from Member a Join Company b on a.Id = b.CompanyId where a.UserId= @UserId", new { UserId = userId}, _uow.CurrentTransaction());
        }
    }
}