using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using CompanyManager.Models.InputModels.Offices;
using CompanyManager.Services.Results;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyManager.Services
{
    public class OfficeService
    {
        private const string CompanyNotFoundMessage = "Company Not Found!";
        private const string OfficeInputModelCreatedMessage = "Created Input Model for Office";
        private const string IncompleteOfficeAddressMessage = "Office address is not complete!";
        private const string OfficeCreatedMessage = "Office was created successfully!";

        private readonly IOfficeRepository officeRepository;
        private readonly ICompanyRepository companyRepository;

        public OfficeService(IOfficeRepository officeRepository, ICompanyRepository companyRepository)
        {
            this.officeRepository = officeRepository;
            this.companyRepository = companyRepository;
        }

        public ResultData<OfficeInputModel> CreateOfficeInputModel(int companyId)
        {
            if (companyId == 0)
            {
                return new ResultData<OfficeInputModel>(CompanyNotFoundMessage, false, null);
            }

            OfficeInputModel inputModel = new OfficeInputModel
            {
                CompanyId = companyId
            };

            return new ResultData<OfficeInputModel>(OfficeInputModelCreatedMessage, true, inputModel);
        }

        public async Task<ResultData<Office>> CreateNewOffice(OfficeInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Country) || string.IsNullOrEmpty(inputModel.Street) || string.IsNullOrEmpty(inputModel.City) || inputModel.StreetNumber == 0)
            {
                return new ResultData<Office>(IncompleteOfficeAddressMessage, false, null);
            }

            Office office = new Office
            {
                Country = inputModel.Country,
                City = inputModel.City,
                Street = inputModel.Street,
                StreetNumber = inputModel.StreetNumber,
                CompanyId = inputModel.CompanyId
            };

            Company company = await this.companyRepository.GetCompanyWithOfficesById(inputModel.CompanyId);

            if (company.Offices.Count() == 0)
            {
                office.IsHeadquarters = true;
            }

            await this.officeRepository.CreateNewOffice(office);

            return new ResultData<Office>(OfficeCreatedMessage, true, office);
        }
    }
}