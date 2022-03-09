using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefits.MVC.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public decimal BenefitsCost { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
