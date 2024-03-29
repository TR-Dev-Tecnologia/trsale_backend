using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Companies;
using TRSale.Domain.Entites;

namespace TRSale.Domain.Interfaces.Services
{
    public interface ICompanyService
    {
        GenericCommandResult Create(CreateCompanyCommand cmd);

    }
}