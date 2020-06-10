using CompanyManager.Models.ViewModels.Employees;
using System.Collections.Generic;

namespace CompanyManager.Models.ViewModels.Offices
{
    public class OfficeDetailsViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public bool IsHeadquarters { get; set; }

        public IEnumerable<EmployeeOfficeDetailsViewModel> Employees { get; set; }
    }
}