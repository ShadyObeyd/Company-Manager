using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyManagerDbContext db;

        public EmployeeRepository(CompanyManagerDbContext db)
        {
            this.db = db;
        }

        public async Task CreateNewEmployee(Employee employee)
        {
            await this.db.Employees.AddAsync(employee);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            this.db.Employees.Remove(employee);
            await this.db.SaveChangesAsync();
        }

        public async Task EditEmployee(Employee employee)
        {
            this.db.Employees.Update(employee);
            await this.db.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = await this.db.Employees.FirstOrDefaultAsync(e => e.Id == id);

            return employee;
        }
    }
}