using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels.Companies
{
    public class CompanyEditModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}