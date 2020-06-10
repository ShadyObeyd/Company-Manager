using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels.Offices
{
    public class OfficeInputModel
    {
        private const string StreetNumberDisplayName = "Street Number";

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = StreetNumberDisplayName)]
        public int StreetNumber { get; set; }

        public int CompanyId { get; set; }
    }
}