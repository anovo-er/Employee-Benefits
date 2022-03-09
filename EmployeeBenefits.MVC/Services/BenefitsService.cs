using EmployeeBenefits.MVC.Core;
using EmployeeBenefits.MVC.Data;
using EmployeeBenefits.MVC.Services.Contracts;
using EmployeeBenefits.MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefits.MVC.Services
{
    public class BenefitsService : IBenefitsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AutoMapper.IConfigurationProvider _mappingConfig;

        public BenefitsService(ApplicationDbContext dbContext, AutoMapper.IConfigurationProvider mappingConfig)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _mappingConfig = mappingConfig;
        }

        public async Task<EmployeeBenefitsViewModel> GetEmployeeBenefitsByEmployeeIdAsync(int employeeId)
        {
            var employee = await _dbContext.Employees.Include(e => e.Dependants).SingleOrDefaultAsync(e => e.Id.Equals(employeeId));
            if (employee == null) return null;

            var mapper = _mappingConfig.CreateMapper();

            var employeeViewModel = mapper.Map(employee, new EmployeeViewModel());
            employeeViewModel.BenefitsCost = BenefitsCalculator.EmployeeBenefitsCost(employeeViewModel.FirstName);

            var dependants = mapper.Map(employee.Dependants, new List<DependantViewModel>());
            dependants.ForEach(d => d.BenefitsCost = BenefitsCalculator.DependantBenefitsCost(d.FirstName));

            var benefits = new EmployeeBenefitsViewModel
            {
                Employee = employeeViewModel,
                Dependants = dependants,
                AnnualSalary = BenefitsCalculator.EmployeeAnnualSalaryBeforeDeductions()
            };

            benefits.PaycheckAfterDeductions = BenefitsCalculator.EmployeePaycheckAfterDeductions(benefits.AnnualBenefitsTotalCost);
            benefits.MonthlyBenefitsTotalCost = BenefitsCalculator.EmployeeMonthlyBenefitsTotalCost(benefits.AnnualBenefitsTotalCost);

            return benefits;
        }
    }
}
