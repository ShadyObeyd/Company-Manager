using CompanyManager.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesWithOffices();

        Task CreateCompany(Company company);
    }
}