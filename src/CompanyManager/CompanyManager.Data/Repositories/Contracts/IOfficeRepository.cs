using CompanyManager.Models.DomainModels;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories.Contracts
{
    public interface IOfficeRepository
    {
        Task CreateNewOffice(Office office);

        Task<Office> GetOfficeWithEmployeesById(int id);

        Task<Office> GetOfficeById(int id);

        Task EditOffice(Office office);

        Task DeleteOffice(Office office);
    }
}