using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels.Companies
{
    public class CreateCompanyInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}