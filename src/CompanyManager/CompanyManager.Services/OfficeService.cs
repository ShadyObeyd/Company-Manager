using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using CompanyManager.Models.InputModels.Offices;
using CompanyManager.Models.ViewModels.Offices;
using CompanyManager.Services.Results;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManager.Services
{
    public class OfficeService
    {
        private const string CompanyNotFoundMessage = "Company Not Found!";
        private const string OfficeInputModelCreatedMessage = "Created Input Model for Office";
        private const string IncompleteOfficeAddressMessage = "Office address is not complete!";
        private const string OfficeCreatedMessage = "Office was created successfully!";
        private const string OfficeNotFoundMessage = "Office Not Found!";
        private const string OfficeDetailsModelCreatedMessage = "Office details view model created successfully!";
        private const string OffceEditModelCreatedMessage = "Office edit model created successfullly!";
        private const string OfficeEditMessage = "Office edited successfully!";
        private const string OfficeDeletedMessage = "Office was deleted!";

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

        public async Task<ResultData<OfficeDetailsViewModel>> CreateDetailsViewModel(int id)
        {
            if (id == 0)
            {
                return new ResultData<OfficeDetailsViewModel>(OfficeNotFoundMessage, false, null);
            }

            Office office = await this.officeRepository.GetOfficeWithEmployeesById(id);

            OfficeDetailsViewModel viewModel = new OfficeDetailsViewModel
            {
                City = office.City,
                Address = $"{office.StreetNumber} {office.Street}, {office.City}, {office.Country}",
                Id = office.Id,
                IsHeadquarters = office.IsHeadquarters,
                Employees = office.Employees.Select(e => new Models.ViewModels.Employees.EmployeeOfficeDetailsViewModel
                {
                    Id = e.Id,
                    FullName = $"{e.FirstName} {e.LastName}"
                })
            };

            return new ResultData<OfficeDetailsViewModel>(OfficeDetailsModelCreatedMessage, true, viewModel);
        }

        public async Task<ResultData<OfficeEditModel>> CreateOfficeEditModel(int id)
        {
            if (id == 0)
            {
                return new ResultData<OfficeEditModel>(OfficeNotFoundMessage, false, null);
            }

            Office office = await this.officeRepository.GetOfficeById(id);

            OfficeEditModel editModel = new OfficeEditModel
            {
                Id = office.Id,
                City = office.City,
                Country = office.Country,
                Street = office.Street,
                StreetNumber = office.StreetNumber
            };

            return new ResultData<OfficeEditModel>(OffceEditModelCreatedMessage, true, editModel);
        }

        public async Task<ResultData<Office>> EditOffice(OfficeEditModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.Country) || string.IsNullOrEmpty(inputModel.Street) || string.IsNullOrEmpty(inputModel.City) || inputModel.StreetNumber == 0)
            {
                return new ResultData<Office>(IncompleteOfficeAddressMessage, false, null);
            }

            Office office = await this.officeRepository.GetOfficeById(inputModel.Id);

            office.Country = inputModel.Country;
            office.City = inputModel.City;
            office.Street = inputModel.Street;
            office.StreetNumber = inputModel.StreetNumber;

            await this.officeRepository.EditOffice(office);

            return new ResultData<Office>(OfficeEditMessage, true, office);
        }

        public async Task<ResultData<int>> DeleteOffice(int id)
        {
            if (id == 0)
            {
                return new ResultData<int>(OfficeNotFoundMessage, false, 0);
            }

            Office office = await this.officeRepository.GetOfficeById(id);

            int companyId = office.CompanyId;

            await this.officeRepository.DeleteOffice(office);

            return new ResultData<int>(OfficeDeletedMessage, true, companyId);
        }
    }
}