using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyManager.Models.DomainModels.Enums;

namespace CompanyManager.Models.DomainModels
{
    public class Employee
    {
        private const string DecimalMaxValue = "79228162514264337593543950335";

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        [Range(typeof(decimal), "0", DecimalMaxValue)]
        public decimal Salary { get; set; }

        [Range(0, int.MaxValue)]
        public int VacationDays { get; set; }


        public ExperienceLevel ExperienceLevel { get; set; }

        public int OfficeId { get; set; }

        [ForeignKey(nameof(OfficeId))]
        public Office Office { get; set; }
    }
}