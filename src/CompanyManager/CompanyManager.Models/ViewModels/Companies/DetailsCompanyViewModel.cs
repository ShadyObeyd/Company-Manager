using CompanyManager.Models.ViewModels.Offices;
using System.Collections.Generic;

namespace CompanyManager.Models.ViewModels.Companies
{
    public class DetailsCompanyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public IEnumerable<OfficeCompanyDetailsViewModel> Offices { get; set; }
    }
}