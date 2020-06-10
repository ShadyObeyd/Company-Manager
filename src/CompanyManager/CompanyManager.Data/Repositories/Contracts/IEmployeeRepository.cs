using CompanyManager.Models.DomainModels;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories.Contracts
{
    public interface IEmployeeRepository
    {
        Task CreateNewEmployee(Employee employee);

        Task<Employee> GetEmployeeById(int id);

        Task EditEmployee(Employee employee);

        Task DeleteEmployee(Employee employee);
    }
}