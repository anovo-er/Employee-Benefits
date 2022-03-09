namespace EmployeeBenefits.MVC.Models
{
    public class Employee : Person
    {
        public virtual List<Dependant> Dependants { get; set; }
    }
}
