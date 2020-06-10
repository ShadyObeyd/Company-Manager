using System.Threading.Tasks;
using CompanyManager.Models.InputModels.Employees;
using CompanyManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Create(int officeId)
        {
            var result = this.employeeService.CreateEmployeeInputModel(officeId);

            if (!result.Success)
            {
                return this.RedirectToAction("NotFound", "Offices");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.employeeService.CreateEmployee(inputModel);

            if (!result.Success)
            {
                return this.View(inputModel);
            }

            //TODO Change to redirect to details page
            return this.View(inputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await this.employeeService.CreateDetailsViewModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }
    }
}