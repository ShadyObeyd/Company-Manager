using System.Threading.Tasks;
using CompanyManager.Models.InputModels.Offices;
using CompanyManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.App.Controllers
{
    public class OfficesController : Controller
    {
        private readonly OfficeService officeService;

        public OfficesController(OfficeService officeService)
        {
            this.officeService = officeService;
        }

        [HttpGet]
        public IActionResult Create(int companyId)
        {
            var result = this.officeService.CreateOfficeInputModel(companyId);

            if (!result.Success)
            {
                return this.RedirectToAction("NotFound", "Companies");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfficeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.officeService.CreateNewOffice(inputModel);

            if (!result.Success)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction("Details", "Companies", new { id = result.Data.CompanyId });
        }
    }
}
