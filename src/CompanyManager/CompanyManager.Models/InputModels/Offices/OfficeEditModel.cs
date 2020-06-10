using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels.Offices
{
    public class OfficeEditModel
    {
        private const string StreetNumberDisplayName = "Street Number";

        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = StreetNumberDisplayName)]
        public int StreetNumber { get; set; }
    }
}