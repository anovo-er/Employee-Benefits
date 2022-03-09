using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBenefits.MVC.Models
{
    public class Dependant : Person
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
