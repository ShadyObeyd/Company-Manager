﻿using CompanyManager.Data.Repositories.Contracts;
using CompanyManager.Models.DomainModels;
using CompanyManager.Models.InputModels.Employees;
using CompanyManager.Services.Results;
using System;
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

        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
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
    }
}