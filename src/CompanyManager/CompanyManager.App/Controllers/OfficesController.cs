﻿using System.Threading.Tasks;
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

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await this.officeService.CreateDetailsViewModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await this.officeService.CreateOfficeEditModel(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OfficeEditModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var result = await this.officeService.EditOffice(inputModel);

            if (!result.Success)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(Details), new { id = result.Data.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.officeService.DeleteOffice(id);

            if (!result.Success)
            {
                return this.View("NotFound");
            }

            return this.RedirectToAction("Details", "Companies", new { id = result.Data });
        }
    }
}