namespace EmployeeBenefits.MVC.Core
{
    public static class BenefitsCalculator
    {
        public static decimal EmployeeBenefitsCost(string employeeName) => employeeName.StartsWith('A') ? decimal.Round((decimal)(Constants.EmployeesBaseBenefitsCost - Constants.EmployeesBaseBenefitsCost * 0.1), 2) : Constants.EmployeesBaseBenefitsCost;
        public static decimal DependantBenefitsCost(string employeeName) => employeeName.StartsWith('A') ? decimal.Round((decimal)(Constants.DependantsBaseBenefitsCost - Constants.DependantsBaseBenefitsCost * 0.1), 2) : Constants.DependantsBaseBenefitsCost;
        public static decimal EmployeePaycheckAfterDeductions(decimal totalDeductions) => decimal.Round(Constants.EmployeesPaycheckBeforeDeductions - totalDeductions / Constants.PaychecksPerYear, 2);
        public static decimal EmployeeMonthlyBenefitsTotalCost(decimal totalBenefitsCost) => decimal.Round(totalBenefitsCost / Constants.PaychecksPerYear, 2);
        public static int EmployeeAnnualSalaryBeforeDeductions() => Constants.EmployeesPaycheckBeforeDeductions * Constants.PaychecksPerYear;
    }
}
