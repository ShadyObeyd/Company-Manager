namespace CompanyManager.Models.ViewModels.Offices
{
    public class OfficeCompanyDetailsViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string Address => $"{this.StreetNumber} {Street}, {City}, {Country}";
    }
}