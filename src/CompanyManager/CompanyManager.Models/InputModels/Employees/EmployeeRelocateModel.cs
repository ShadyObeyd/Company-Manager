using CompanyManager.Models.ViewModels.Offices;
using System.Collections.Generic;

namespace CompanyManager.Models.InputModels.Employees
{
    public class EmployeeRelocateModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int NewOfficeId { get; set; }

        public IEnumerable<RelocateEmployeeOfficeModel> CompanyOffices { get; set; }
    }
}