using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Entites;

namespace TRSale.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository: IBaseRepository<Company>
    {
        IEnumerable<Company> MyCompanies(Guid userId);
    }
}