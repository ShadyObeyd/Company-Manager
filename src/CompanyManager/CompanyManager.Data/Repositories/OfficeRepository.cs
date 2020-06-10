using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly CompanyManagerDbContext db;

        public OfficeRepository(CompanyManagerDbContext db)
        {
            this.db = db;
        }

        public async Task CreateNewOffice(Office office)
        {
            await this.db.Offices.AddAsync(office);
            await this.db.SaveChangesAsync();
        }

        public async Task<Office> GetOfficeWithEmployeesById(int id)
        {
            Office office = await this.db.Offices.Include(o => o.Employees).FirstOrDefaultAsync(o => o.Id == id);

            return office;
        }

        public async Task<Office> GetOfficeById(int id)
        {
            Office office = await this.db.Offices.FirstOrDefaultAsync(o => o.Id == id);

            return office;
        }

        public async Task EditOffice(Office office)
        {
            this.db.Offices.Update(office);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteOffice(Office office)
        {
            this.db.Offices.Remove(office);
            await this.db.SaveChangesAsync();
        }
    }
}