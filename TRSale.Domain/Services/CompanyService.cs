using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TRSale.Domain.Commands;
using TRSale.Domain.Commands.Companies;
using TRSale.Domain.Entites;
using TRSale.Domain.Interfaces.Infra;
using TRSale.Domain.Interfaces.Repositories;
using TRSale.Domain.Interfaces.Services;

namespace TRSale.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseRepository<Company> _companyRepository;        
        private readonly IBaseRepository<Member> _memberRepository;
        private readonly IUnitOfWork _uow;

        public CompanyService(IBaseRepository<Company> companyRepository, IBaseRepository<Member> memberRepository, IUnitOfWork uow)
        {
            _companyRepository = companyRepository;
            _uow = uow;
            _memberRepository = memberRepository;
        }

        public GenericCommandResult Create(CreateCompanyCommand cmd)
        {
            _uow.BeginTransaction();
            try
            {
                var company = new Company(cmd.Name);
                _companyRepository.Create(company);
                var member = new Member(company.Id, cmd.UserId);
                _memberRepository.Create(member);
                _uow.Commit();
                return new GenericCommandResult(true, "Company Create with success");
            }
            catch(Exception)
            {
                _uow.Rollback();
                throw;
            }
        }
    }
}