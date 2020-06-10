using CompanyManager.Models.DomainModels;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories.Contracts
{
    public interface IOfficeRepository
    {
        Task CreateNewOffice(Office office);
    }
}