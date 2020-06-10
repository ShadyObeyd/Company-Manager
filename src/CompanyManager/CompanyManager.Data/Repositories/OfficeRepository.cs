using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
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
    }
}