using System.Threading.Tasks;
using CompanyManager.Models.DomainModels;
using CompanyManager.Models.InputModels.Companies;
using CompanyManager.Services;
using CompanyManager.Services.Results;
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

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await this.companyService.CreateCompanyDetailsViewModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.companyService.CreateEditModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyEditModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.companyService.EditCompanyName(inputModel.Id, inputModel.Name);

            if (!result.Success)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Result result = await this.companyService.DeleteCompany(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.RedirectToAction(nameof(ViewCompanies));
        }
    }
}