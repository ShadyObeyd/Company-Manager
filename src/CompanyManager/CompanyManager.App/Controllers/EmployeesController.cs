﻿using System.Threading.Tasks;
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

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.employeeService.CreateEmployeeEditModel(id);

            if (!result.Success)
            {
                return this.View("Not Found");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.employeeService.EditEmployee(inputModel);

            if (!result.Success)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.employeeService.DeleteEmployee(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.RedirectToAction("Details", "Offices", new { id = result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> Relocate(int id)
        {
            var result = await this.employeeService.CreateRelocateModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Relocate(EmployeeRelocateModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.employeeService.RelocateEmployee(inputModel);

            if (!result.Success)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction("Details", "Offices", new { id = result.Data.OfficeId });
        }
    }
}