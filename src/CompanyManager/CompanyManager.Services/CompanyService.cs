using CompanyManager.Models.DomainModels;
using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Services.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyManager.Models.ViewModels.Companies;
using System.Linq;
using System.Globalization;

namespace CompanyManager.Services
{
    public class CompanyService
    {
        private const string EmptyCompanyNameMessage = "Company name cannot be empty!";
        private const string CompanyCreatedSuccessfullyMessage = "Company created successfully";
        private const string CompaniesFoundMessage = "Found all registered companies.";
        private const string DateFormat = "dd.MM.yyyy";

        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<ResultData<IEnumerable<AllCompaniesViewModel>>> CreateAllCompaniesViewModel()
        {
            IEnumerable<Company> companies = await this.companyRepository.GetAllCompaniesWithOffices();

            IEnumerable<AllCompaniesViewModel> viewModel = companies.Select(c => new AllCompaniesViewModel
            {
                Id = c.Id,
                Name = c.Name,
                CreatedOn = c.CreationDate.ToString(DateFormat, CultureInfo.InvariantCulture),
                OfficesCount = c.Offices.Count()
            });

            return new ResultData<IEnumerable<AllCompaniesViewModel>>(CompaniesFoundMessage, true, viewModel);
        }

        public async Task<Result> CreateCompany(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new Result(EmptyCompanyNameMessage, false);
            }

            Company company = new Company
            {
                Name = name,
                CreationDate = DateTime.Today
            };

            await this.companyRepository.CreateCompany(company);

            return new Result(CompanyCreatedSuccessfullyMessage, true);
        }
    }
}