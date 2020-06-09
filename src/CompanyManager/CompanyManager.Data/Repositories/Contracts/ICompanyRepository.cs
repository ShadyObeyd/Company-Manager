using CompanyManager.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesWithOffices();

        Task CreateCompany(Company company);

        Task<Company> GetCompanyWithOfficesById(int id);

        Task EditCompanyName(Company company, string newName);

        Task<Company> GetCompanyById(int id);

        Task DeleteCompany(Company company);
    }
}