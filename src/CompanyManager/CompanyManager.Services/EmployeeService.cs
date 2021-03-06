﻿using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using CompanyManager.Models.InputModels.Employees;
using CompanyManager.Models.ViewModels.Employees;
using CompanyManager.Models.ViewModels.Offices;
using CompanyManager.Services.Results;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManager.Services
{
    public class EmployeeService
    {
        private const string EmployeeInputModelCreatedMessage = "Employee input model created.";
        private const string OfficeNotFoundMessage = "Office not found!";
        private const string EmployeeNamesNotFoundMessage = "Employees should have first and last name!";
        private const string InvalidStartDateMessage = "Starting date must be equal or larger than today!";
        private const string InvalidSalaryMessage = "Salary must be larger than zero!";
        private const string EmployeeAddedMessage = "Employee added successfully.";
        private const string EmployeeNotFoundMessage = "Employee does not exist!";
        private const string DateFormat = "dd.MM.yyyy";
        private const string DetailsViewModelCreatedMessage = "Employee details model created.";
        private const string EmployeeEditModelCreatedMessage = "Employee edit model was created.";
        private const string EmployeeUpdatedMessage = "Employee was updated.";
        private const string EmployeeDeletedMessage = "Employee was deleted.";
        private const string EmployeeRelocateModelCreatedMessage = "Employee relocate moded was created.";

        private readonly IEmployeeRepository employeeRepository;
        private readonly IOfficeRepository officeRepository;
        private readonly ICompanyRepository companyRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IOfficeRepository officeRepository, ICompanyRepository companyRepository)
        {
            this.employeeRepository = employeeRepository;
            this.officeRepository = officeRepository;
            this.companyRepository = companyRepository;
        }

        public ResultData<EmployeeInputModel> CreateEmployeeInputModel(int officeId)
        {
            if (officeId == 0)
            {
                return new ResultData<EmployeeInputModel>(OfficeNotFoundMessage, false, null);
            }

            var model = new EmployeeInputModel
            {
                OfficeId = officeId,
                StartingDate = DateTime.Today
            };

            return new ResultData<EmployeeInputModel>(EmployeeInputModelCreatedMessage, true, model);
        }

        public async Task<ResultData<Employee>> CreateEmployee(EmployeeInputModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.FirstName) || string.IsNullOrEmpty(inputModel.LastName))
            {
                return new ResultData<Employee>(EmployeeNamesNotFoundMessage, false, null);
            }

            if (inputModel.StartingDate < DateTime.Today)
            {
                return new ResultData<Employee>(InvalidStartDateMessage, false, null);
            }

            if (inputModel.Salary < 0)
            {
                return new ResultData<Employee>(InvalidSalaryMessage, false, null);
            }

            Employee employee = new Employee
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                ExperienceLevel = inputModel.ExperienceLevel,
                Salary = inputModel.Salary,
                StartingDate = inputModel.StartingDate,
                VacationDays = 20,
                OfficeId = inputModel.OfficeId
            };

            await this.employeeRepository.CreateNewEmployee(employee);

            return new ResultData<Employee>(EmployeeAddedMessage, true, employee);
        }

        public async Task<ResultData<EmployeeDetailsViewModel>> CreateDetailsViewModel(int id)
        {
            if (id == 0)
            {
                return new ResultData<EmployeeDetailsViewModel>(EmployeeNotFoundMessage, false, null);
            }

            Employee employee = await this.employeeRepository.GetEmployeeById(id);

            EmployeeDetailsViewModel viewModel = new EmployeeDetailsViewModel
            {
                FullName = $"{employee.FirstName} {employee.LastName}",
                ExpLevel = employee.ExperienceLevel.ToString(),
                Salary = employee.Salary,
                StartingDate = employee.StartingDate.ToString(DateFormat, CultureInfo.InvariantCulture),
                Id = employee.Id
            };

            return new ResultData<EmployeeDetailsViewModel>(DetailsViewModelCreatedMessage, true, viewModel);
        }

        public async Task<ResultData<EmployeeEditModel>> CreateEmployeeEditModel(int id)
        {
            Employee employee = await this.employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return new ResultData<EmployeeEditModel>(EmployeeNotFoundMessage, false, null);
            }

            EmployeeEditModel model = new EmployeeEditModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ExperienceLevel = employee.ExperienceLevel,
                Salary = employee.Salary
            };

            return new ResultData<EmployeeEditModel>(EmployeeEditModelCreatedMessage, true, model);
        }

        public async Task<ResultData<Employee>> EditEmployee(EmployeeEditModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.FirstName) || string.IsNullOrEmpty(inputModel.LastName))
            {
                return new ResultData<Employee>(EmployeeNamesNotFoundMessage, false, null);
            }

            if (inputModel.Salary < 0)
            {
                return new ResultData<Employee>(InvalidSalaryMessage, false, null);
            }

            Employee employee = await this.employeeRepository.GetEmployeeById(inputModel.Id);

            employee.FirstName = inputModel.FirstName;
            employee.LastName = inputModel.LastName;
            employee.Salary = inputModel.Salary;
            employee.ExperienceLevel = inputModel.ExperienceLevel;

            await this.employeeRepository.EditEmployee(employee);

            return new ResultData<Employee>(EmployeeUpdatedMessage, true, employee);
        }

        public async Task<ResultData<int>> DeleteEmployee(int id)
        {
            if (id == 0)
            {
                return new ResultData<int>(EmployeeNotFoundMessage, false, 0);
            }

            Employee employee = await this.employeeRepository.GetEmployeeById(id);

            int officeId = employee.OfficeId;

            await this.employeeRepository.DeleteEmployee(employee);

            return new ResultData<int>(EmployeeDeletedMessage, true, officeId);
        }

        public async Task<ResultData<EmployeeRelocateModel>> CreateRelocateModel(int id)
        {
            if (id == 0)
            {
                return new ResultData<EmployeeRelocateModel>(EmployeeNotFoundMessage, false, null);
            }

            Employee employee = await this.employeeRepository.GetEmployeeById(id);
            Office office = await this.officeRepository.GetOfficeById(employee.OfficeId);
            Company company = await this.companyRepository.GetCompanyWithOfficesById(office.CompanyId);

            EmployeeRelocateModel model = new EmployeeRelocateModel
            {
                Id = employee.Id,
                FullName = $"{employee.FirstName} {employee.LastName}",
                CompanyOffices = company.Offices.Select(o => new RelocateEmployeeOfficeModel
                {
                    Id = o.Id,
                    Address = $"{o.StreetNumber} {o.Street}, {o.City}, {o.Country}"
                })
            };

            return new ResultData<EmployeeRelocateModel>(EmployeeRelocateModelCreatedMessage, true, model);
        }

        public async Task<ResultData<Employee>> RelocateEmployee(EmployeeRelocateModel inputModel)
        {
            if (inputModel.NewOfficeId == 0)
            {
                return new ResultData<Employee>(OfficeNotFoundMessage, false, null);
            }

            Employee employee = await this.employeeRepository.GetEmployeeById(inputModel.Id);

            employee.OfficeId = inputModel.NewOfficeId;

            await this.employeeRepository.EditEmployee(employee);

            return new ResultData<Employee>(EmployeeUpdatedMessage, true, employee);
        }
    }
}