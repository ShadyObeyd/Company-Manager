using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels
{
    public class CreateCompanyInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}