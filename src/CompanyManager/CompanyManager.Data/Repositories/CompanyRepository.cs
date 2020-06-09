﻿using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManager.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyManagerDbContext db;
        public CompanyRepository(CompanyManagerDbContext db)
        {
            this.db = db;
        }

        public async Task CreateCompany(Company company)
        {
            await this.db.Companies.AddAsync(company);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesWithOffices()
        {
            IEnumerable<Company> companies = await this.db.Companies.Include(c => c.Offices).ToArrayAsync();

            return companies;
        }

        public async Task<Company> GetCompanyWithOfficesById(int id)
        {
            Company company = await this.db.Companies.Include(c => c.Offices).FirstOrDefaultAsync(c => c.Id == id);

            return company;
        }
    }
}