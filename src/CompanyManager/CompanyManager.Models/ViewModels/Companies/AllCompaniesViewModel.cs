namespace CompanyManager.Models.ViewModels.Companies
{
    public class AllCompaniesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CreatedOn { get; set; }

        public int OfficesCount { get; set; }
    }
}