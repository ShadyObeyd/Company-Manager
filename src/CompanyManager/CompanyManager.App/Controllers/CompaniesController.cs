using System.Threading.Tasks;
using CompanyManager.Models.InputModels;
using CompanyManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.App.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly CompanyService companyService;

        public CompaniesController(CompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewCompanies()
        {
            var result = await this.companyService.CreateAllCompaniesViewModel();

            return this.View(result.Data);
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var result = await this.companyService.CreateCompany(inputModel.Name);

            if (!result.Success)
            {
                return this.View();
            }

            return this.Redirect(nameof(ViewCompanies));
        }
    }
}