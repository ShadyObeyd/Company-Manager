namespace CompanyManager.Models.ViewModels.Employees
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string ExpLevel { get; set; }

        public decimal Salary { get; set; }

        public string StartingDate { get; set; }
    }
}