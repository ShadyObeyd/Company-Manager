using CompanyManager.Models.DomainModels.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models.InputModels.Employees
{
    public class EmployeeInputModel
    {
        private const string DecimalMaxValue = "79228162514264337593543950335";
        private const string DisplayFirstName = "First Name";
        private const string DisplayLastName = "Last Name";
        private const string DisplayStartingDate = "Starting Date";
        private const string DisplayExpLevel = "Experience Level";

        [Required]
        [Display(Name = DisplayFirstName)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = DisplayLastName)]
        public string LastName { get; set; }

        [Display(Name = DisplayStartingDate)]
        public DateTime StartingDate { get; set; }

        [Range(typeof(decimal), "0", DecimalMaxValue)]
        public decimal Salary { get; set; }

        [Display(Name = DisplayExpLevel)]
        public ExperienceLevel ExperienceLevel { get; set; }

        public int OfficeId { get; set; }
    }
}