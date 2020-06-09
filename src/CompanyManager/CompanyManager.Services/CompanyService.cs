using CompanyManager.Models.DomainModels;
using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Services.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyManager.Models.ViewModels.Companies;
using System.Linq;
using System.Globalization;
using CompanyManager.Models.ViewModels.Offices;
using CompanyManager.Models.InputModels.Companies;

namespace CompanyManager.Services
{
    public class CompanyService
    {
        private const string EmptyCompanyNameMessage = "Company name cannot be empty!";
        private const string CompanyCreatedSuccessfullyMessage = "Company created successfully";
        private const string CompaniesFoundMessage = "Found all registered companies.";
        private const string DateFormat = "dd.MM.yyyy";
        private const string CompanyNotFoundMessage = "Company does not exist!";
        private const string CompanyFoundMessage = "Found the wanted company.";
        private const string CompanyNameUpdatedMessage = "Company name changed.";
        private const string CompanyEditModelCreatedMessage = "Created company edit model.";
        private const string CompanyDeletedMessage = "Company deleted successfully.";

        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<ResultData<IEnumerable<AllCompaniesViewModel>>> CreateAllCompaniesViewModel()
        {
            var companies = await this.companyRepository.GetAllCompaniesWithOffices();

            var viewModel = companies.Select(c => new AllCompaniesViewModel
            {
                Id = c.Id,
                Name = c.Name,
                CreatedOn = c.CreationDate.ToString(DateFormat, CultureInfo.InvariantCulture),
                OfficesCount = c.Offices.Count()
            });

            return new ResultData<IEnumerable<AllCompaniesViewModel>>(CompaniesFoundMessage, true, viewModel);
        }

        public async Task<ResultData<Company>> CreateCompany(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ResultData<Company>(EmptyCompanyNameMessage, false, null);
            }

            Company company = new Company
            {
                Name = name,
                CreationDate = DateTime.Today
            };

            await this.companyRepository.CreateCompany(company);

            return new ResultData<Company>(CompanyCreatedSuccessfullyMessage, true, company);
        }

        public async Task<ResultData<DetailsCompanyViewModel>> CreateCompanyDetailsViewModel(int id)
        {
            Company company = await this.companyRepository.GetCompanyWithOfficesById(id);

            if (company == null)
            {
                return new ResultData<DetailsCompanyViewModel>(CompaniesFoundMessage, false, null);
            }

            DetailsCompanyViewModel viewModel = new DetailsCompanyViewModel
            {
                Id = company.Id,
                Name = company.Name,
                CreatedOn = company.CreationDate.ToString(DateFormat, CultureInfo.InvariantCulture),
                Offices = company.Offices.Select(o => new OfficeCompanyDetailsViewModel
                {
                    Id = o.Id,
                    City = o.City,
                    Country = o.Country,
                    Street = o.Street,
                    StreetNumber = o.StreetNumber
                })
            };

            return new ResultData<DetailsCompanyViewModel>(CompanyFoundMessage, true, viewModel);
        }

        public async Task<ResultData<Company>> EditCompanyName(int id, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                return new ResultData<Company>(EmptyCompanyNameMessage, false, null);
            }

            Company company = await this.companyRepository.GetCompanyById(id);

            if (company == null)
            {
                return new ResultData<Company>(CompanyNotFoundMessage, false, null);
            }

            await this.companyRepository.EditCompanyName(company, newName);

            return new ResultData<Company>(CompanyNameUpdatedMessage, true, company);
        }

        public async Task<ResultData<CompanyEditModel>> CreateEditModel(int id)
        {
            Company company = await this.companyRepository.GetCompanyById(id);

            if (company == null)
            {
                return new ResultData<CompanyEditModel>(CompanyNotFoundMessage, false, null);
            }

            CompanyEditModel model = new CompanyEditModel
            {
                Id = company.Id,
                Name = company.Name
            };

            return new ResultData<CompanyEditModel>(CompanyEditModelCreatedMessage, true, model);
        }

        public async Task<Result> DeleteCompany(int id)
        {
            Company company = await this.companyRepository.GetCompanyById(id);

            if (company == null)
            {
                new Result(CompanyNotFoundMessage, false);
            }

            await this.companyRepository.DeleteCompany(company);

            return new Result(CompanyDeletedMessage, true);
        }
    }
}