using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefits.MVC.ViewModels
{
    public class DependantViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public decimal BenefitsCost { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
