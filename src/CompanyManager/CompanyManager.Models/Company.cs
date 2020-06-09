using CompanyManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Data
{
    public class Company
    {
        public Company()
        {
            this.Offices = new HashSet<Office>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<Office> Offices { get; set; }
    }
}