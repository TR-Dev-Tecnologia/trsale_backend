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
        private readonly ICompanyRepository _companyRepository;        
        private readonly IBaseRepository<Member> _memberRepository;

        private readonly ICompanyEnviroment _companyEnviroment;
        private readonly IUnitOfWork _uow;

        public CompanyService(
            ICompanyRepository companyRepository, 
            IBaseRepository<Member> memberRepository,
            ICompanyEnviroment companyEnviroment,
            IUnitOfWork uow)
        {
            _companyRepository = companyRepository;
            _uow = uow;
            _memberRepository = memberRepository;
            _companyEnviroment = companyEnviroment;
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
                _companyEnviroment.Create(company.Id);
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