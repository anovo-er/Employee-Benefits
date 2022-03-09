namespace EmployeeBenefits.MVC.ViewModels
{
    public class EmployeeBenefitsViewModel
    {
        public EmployeeViewModel Employee { get; set; }
        public List<DependantViewModel> Dependants { get; set; }
        public int AnnualSalary { get; set; }
        public decimal AnnualBenefitsTotalCost => decimal.Round(Employee.BenefitsCost + Dependants.Sum(d => d.BenefitsCost), 2);
        public decimal MonthlyBenefitsTotalCost { get; set; }
        public decimal PaycheckAfterDeductions { get; set; }
        public EmployeeBenefitsViewModel() => Dependants = new List<DependantViewModel>();
    }
}
