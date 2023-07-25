using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TRSale.Domain.Interfaces.Infra
{
    public interface ICompanyEnviroment
    {
        void Create(Guid companyId);
    }
}